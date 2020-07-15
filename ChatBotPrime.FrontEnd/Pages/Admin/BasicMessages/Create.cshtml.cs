using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Infra.Data.EF;
using ChatBotPrime.Core.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChatBotPrime.FrontEnd.Pages.Admin.BasicMessages
{
	public class CreateModel : PageModel
	{
		private readonly IRepository _repository;

		public CreateModel(IRepository repository)
		{
			_repository = repository;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public BasicMessage BasicMessage { get; set; }

		
		public async Task<IActionResult> OnPostAsync()
		{
			var emptyBasicMessage = new BasicMessage();

			if (await TryUpdateModelAsync(
				emptyBasicMessage,
				"BasicMessage",
				bm => bm.MessageText,
				bm => bm.Response
			))
			{
				await _repository.CreateAsync(emptyBasicMessage);

				return RedirectToPage("./Index");

			}

			return Page();
		}
	}
}
