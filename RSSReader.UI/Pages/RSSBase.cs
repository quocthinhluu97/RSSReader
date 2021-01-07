using Microsoft.AspNetCore.Components;
using RSSReader.Shared;
using RSSReader.UI.Components;
using RSSReader.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSReader.UI.Pages
{
    public class RSSBase : ComponentBase
    {
        [Inject]
        public IRssDataService RssService { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        [CascadingParameter(Name ="ErrorComponent")]
        protected IErrorComponent ErrorComponent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //await base.OnInitializedAsync();
                Posts = (await RssService.GetNewsFeed()).ToList();

                //StateHasChanged();
            }
            catch (Exception e)
            {
                ErrorComponent.ShowError(e.Message, e.StackTrace);
            }
        }
    }
}
