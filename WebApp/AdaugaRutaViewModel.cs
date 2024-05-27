using System.ComponentModel.DataAnnotations;
namespace WebApp;
public class AdaugaRutaViewModel
{
    [Required(ErrorMessage = "Oras is required")]
    public string Oras { get; set; }
    
    [Required(ErrorMessage = "Orar is required")]
    public string Orar { get; set; }

   
}