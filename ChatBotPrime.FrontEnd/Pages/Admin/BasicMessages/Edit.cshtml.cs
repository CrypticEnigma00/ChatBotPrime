using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Infra.Data.EF;
using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Data.Specifications;

namespace ChatBotPrime.FrontEnd.Pages.Admin.BasicMessages
{
    public class EditModel : PageModel
    {
        private IRepository _repository;

        public EditModel(IRepository repository)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.UpdateAsync(BasicMessage);

            return RedirectToPage("./Index");
        }

    }
}
