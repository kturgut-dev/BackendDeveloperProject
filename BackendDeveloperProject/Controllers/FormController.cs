using BackendDeveloperProject.Core.Extensions;
using BackendDeveloperProject.Entities.Concrete;
using BackendDeveloperProject.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading;

namespace BackendDeveloperProject.UI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [Route("forms")]
    public class FormController : Controller
    {
        private readonly IFormService _formService;
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> Index(string name = null, CancellationToken cancellationToken = default)
        {
            ViewData["Title"] = "Form Yönetim";

            DataResult<IEnumerable<Form>> result = !string.IsNullOrEmpty(name) ?
                await _formService.GetListAsync(x => x.Name.Contains(name), cancellationToken) :
                await _formService.GetListAsync( cancellationToken);

            return View(result);
        }


        [HttpGet("{id}")]
        [Route("{id}")]
        public async Task<IActionResult> Preview(long id, CancellationToken cancellationToken = default)
        {
            DataResult<Form> resuılt = await _formService.GetAsync(id, cancellationToken);
            if (resuılt.IsSuccess)
            {
                ViewData["Title"] = "Form Yönetim | " + (resuılt.Data?.Name ?? id.ToString());
                return View(resuılt.Data);
            }
            else
                return Redirect(nameof(Index));
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Form Ekle";
            return View(nameof(Form));
        }

        [HttpGet("{id:long?}/[action]")]
        public async Task<IActionResult> Form(long? id, CancellationToken cancellationToken = default)
        {
            if (!id.HasValue)
                return RedirectToAction(nameof(Index));

            ViewData["Title"] = "Form Düzenle";
            DataResult<Form> result = await _formService.GetAsync(id.Value, cancellationToken);
            if (!result.IsSuccess)
                return RedirectToAction(nameof(Index));
            else
                return View(result.Data);
        }

        [HttpPost("{id:long?}/[action]")]
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Form(Form formData, long? id = null, CancellationToken cancellationToken = default)
        {
            if (formData?.Fields != null)
            {
                ((List<FormField>)formData.Fields).RemoveAll(x => string.IsNullOrEmpty(x.Name));
            }


            if (id.HasValue)
            {
                Result result = await _formService.UpdateAsync(formData, cancellationToken);
                if (result.IsSuccess)
                {
                    TempData["ResultMessage"] = result.Message.IsNullOrEmpty("İçerik başarıyla güncellendi.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string message = result.Message.IsNullOrEmpty("İçerik güncellenirken bir hata oluştu.");
                    ModelState.AddModelError("GlobalError", message);
                    return View(formData);
                }
            }
            else
            {
                DataResult<Form> result = await _formService.AddAsync(formData, cancellationToken);
                if (result.IsSuccess)
                {
                    TempData["ResultMessage"] = result.Message.IsNullOrEmpty("İçerik başarıyla eklendi.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string message = result.Message.IsNullOrEmpty("İçerik eklenirken bir hata oluştu.");
                    ModelState.AddModelError("GlobalError", message);
                    return View(formData);
                }
            }
        }

        [HttpDelete("{id:long}/[action]")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            return Json(await _formService.DeleteAsync(id, cancellationToken));
        }
    }
}
