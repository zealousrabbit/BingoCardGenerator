using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BingoCardGenerator.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int Size { get; set; }

        [BindProperty]
        public int NumCards { get; set; }

        [BindProperty]
        public string Phrases { get; set; }

        [BindProperty]
        public string FreeSpace { get; set; }

        [BindProperty]
        public string CardTitle { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var phrasesList = Phrases.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(p => p.Trim())
                                     .ToList();
            if (phrasesList.Count == 0)
            {
                return Content("No phrases provided.");
            }

            var cardsHtml = GenerateBingoCards(Size, NumCards, phrasesList, FreeSpace, CardTitle);
            return Content(cardsHtml, "text/html");
        }

        private string GenerateBingoCards(int size, int numCards, List<string> phrases, string freeSpace, string cardTitle)
        {
            var htmlBuilder = new System.Text.StringBuilder();

            for (int cardIndex = 0; cardIndex < numCards; cardIndex++)
            {
                htmlBuilder.Append("<div class='card'>");

                if (!string.IsNullOrEmpty(cardTitle))
                {
                    htmlBuilder.Append($"<div class='card-title'>{cardTitle}</div>");
                }

                htmlBuilder.Append("<table>");

                for (int row = 0; row < size; row++)
                {
                    htmlBuilder.Append("<tr>");
                    for (int col = 0; col < size; col++)
                    {
                        if (row == size / 2 && col == size / 2)
                        {
                            htmlBuilder.Append($"<td class='free-space'>{freeSpace}</td>");
                        }
                        else
                        {
                            var phrase = phrases.ElementAtOrDefault((row * size + col) % phrases.Count);
                            htmlBuilder.Append($"<td>{phrase}</td>");
                        }
                    }
                    htmlBuilder.Append("</tr>");
                }

                htmlBuilder.Append("</table>");
                htmlBuilder.Append("</div>");
            }

            return htmlBuilder.ToString();
        }
    }
}
