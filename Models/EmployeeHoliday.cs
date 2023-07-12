using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrProject.Models;

[PrimaryKey(nameof(Holiday),nameof(Genral_Id))]
public class EmployeeHoliday
{
    public string Holiday { get; set; }

    [ForeignKey("GenrealSetting")]
    public int Genral_Id { get; set; }
    public GeneralSetting? GenrealSetting { get; set; }
}
