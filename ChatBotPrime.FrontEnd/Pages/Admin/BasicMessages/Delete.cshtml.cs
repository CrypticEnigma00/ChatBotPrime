using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Data.Specifications;

namespace ChatBotPrime.FrontEnd.Pages.Admin.BasicMessages
{
	public class DeleteModel : PageModel
    {
        private IRepository _repository;

        public DeleteModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public BasicMessage BasicMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BasicMessage = await _repository.SingleAsync(BasicMessagePolicy.ById((Guid)id));

            if (BasicMessage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BasicMessage = await _repository.SingleAsync(BasicMessagePolicy.ById((Guid)id));

            if (BasicMessage != null)
            {
                await _repository.RemoveAsync(BasicMessage);
            }

            return RedirectToPage("./Index");
        }
    }
}
