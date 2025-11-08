using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMyAnimeList.Models;

public class UpdateAnimationStudioRequest
{
    public int UpdateStudioId { get; set; }
    public string UpdateName { get; set; }
    public int UpdateYear { get; set; }
}
