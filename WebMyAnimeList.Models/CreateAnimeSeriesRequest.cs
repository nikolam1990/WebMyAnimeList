using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebMyAnimeList.Models;

public class CreateAnimeSeriesRequest
{
    public string Name { get; set; }
    public int Season { get; set; }
    public int Number { get; set; }
    public int AnimeId { get; set; }
    public int StudioId { get; set; }
}
