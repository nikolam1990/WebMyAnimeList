using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebMyAnimeList.Models
{
    public class UpdateAnimeSeriesRequest
    {
        public int EpisodeId { get; set; }
        public int AnimeId { get; set; }
        public string Name { get; set; }
        public int CuontSezon { get; set; }
        public int CuontSerios { get; set; }
        public int Studios { get; set; }

    }
}
