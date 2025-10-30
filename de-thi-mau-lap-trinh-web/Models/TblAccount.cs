using System;
using System.Collections.Generic;

namespace de_thi_mau_lap_trinh_web.Models;

public partial class TblAccount
{
    public int Uid { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
