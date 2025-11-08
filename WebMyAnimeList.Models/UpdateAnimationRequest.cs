using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMyAnimeList.Models
{
    public class UpdateAnimationRequest
    {
        public int AnimeId { get; set; }
        public string Name { get; set; }
        public int CuontSezon { get; set; }
        public int CuontSerios { get; set; }
        public Genre[] GenreAnime { get; set; }
        public List<int> Studios { get; set; }
    }
}
