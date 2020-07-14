using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Infra.Data.EF;
using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Data.Specifications;

namespace ChatBotPrime.FrontEnd.Pages.Admin.BasicMessages
{
    public class DetailsModel : PageModel
    {
        private IRepository _repository;

        public DetailsModel(IRepository repository)
        {
            _repository = repository;
        }

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
    }
}
