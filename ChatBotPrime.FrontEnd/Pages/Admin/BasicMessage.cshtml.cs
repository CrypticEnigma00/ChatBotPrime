using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Core.Data.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatBotPrime.FrontEnd.Pages.Admin
{
	public class BasicMessageModel : PageModel
	{
		private IRepository _repository;

		public BasicMessageModel(IRepository repository)
		{
			_repository = repository;
		}

		public IList<BasicMessage> BasicMessages { get; set; }

		public async Task OnGetAsync()
		{
			BasicMessages = await _repository.ListAsync(BasicMessagePolicy.All());
		}

		public async Task OnPostAsync(BasicMessage message)
		{
			await _repository.CreateAsync(message);
		}

		public async Task OnPutAsync(BasicMessage message)
		{
			await _repository.UpdateAsync(message);
		}

		public async Task OnDeleteAsync(BasicMessage message)
		{
			await _repository.RemoveAsync(message);
		}
	}
}
