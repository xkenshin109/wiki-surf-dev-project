using LinqToWiki;
using LinqToWiki.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiSurfModel;

namespace WikiProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = System.IO.File.ReadAllText("D:\\Eclips projects\\wiki-surf-dev-project\\WikiSurfCore\\WikiProcess\\words.txt").Replace("\r","").Split('\n');
            using(var db = new WikiSurfCore.WikiSurfContext())
            {
                var wiki = new Wiki("TheNameOfMyBot/1.0", "en.wikipedia.org", "https://en.wikipedia.org/w/api.php");


                foreach (var row in text)
                {
                    var pages = wiki.Query.search(row.Trim())
                        .Select(x => x.title)
                        .ToList()
                        .Where(x=>x.Trim().ToLower() == row.Trim().ToLower())
                        .ToList();
                    if(pages.Count > 0)
                    {
                        if(db.WordBanks.FirstOrDefault(x=>x.Word == row.Trim()) == null)
                        {
                            db.WordBanks.Add(WordBank.AddWord(row.Trim()));
                            db.SaveChanges();
                        }                        
                    }   
                }
                
            }
        }
    }
}
