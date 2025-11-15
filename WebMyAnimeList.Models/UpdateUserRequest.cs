using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMyAnimeList.Models;

public class UpdateUserRequest
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //public List<AnimeSeries> AnimeSeries { get; set; }
}

