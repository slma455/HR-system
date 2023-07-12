using System.ComponentModel.DataAnnotations.Schema;

namespace HrProject.Models
{
	public class GeneralSetting
	{
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ValueExtra { get; set; }
        public int ValueDiscount { get; set; }
        public virtual List<EmployeeHoliday>? DayOff { get; set; } = new List<EmployeeHoliday>();
    }
}
