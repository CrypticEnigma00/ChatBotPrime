using System.Collections.Generic;
using System.Threading.Tasks;
using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Core.Data.Specifications;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChatBotPrime.FrontEnd.Pages.Admin.BasicMessages
{
	public class IndexModel : PageModel
	{
		private IRepository _repository;

		public IndexModel(IRepository repository)
		{
			_repository = repository;
		}

		public IList<BasicMessage> BasicMessage { get;set; }

		public async Task OnGetAsync()
		{
			BasicMessage = await _repository.ListAsync(BasicMessagePolicy.All());
		}
	}
}
