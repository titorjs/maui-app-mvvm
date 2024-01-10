using System.ComponentModel.DataAnnotations;

namespace MVVMauiApp.Model
{
    public class Global
    {
        [Key]
        public int Glo_id { get; set; }

        [Required, MaxLength(50)]
        public string Glo_nombre { get; set; }

        [Required]
        public int Glo_valor {  get; set; }
    }
}
