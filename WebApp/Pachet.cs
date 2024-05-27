namespace WebApp
{
    public class Pachet
    {
        public string NumeExpeditor { get; set; }
        public string TelefonExpeditor { get; set; }
        public string OrasPlecare { get; set; }
        
        public string OrasDestinatie { get; set; }
        public string NumeDestinatar { get; set; }
        public string TelefonDestinatar { get; set; }
        public double Greutate { get; set; }
        public string CategorieSpeciala { get; set; }
        
        // Metodă pentru calculul costului
        public double CalculeazaCost()
        {
            double cost = 0;

            // Cost bazat pe greutate
            cost += Greutate * 5; // exemplu: 10 unități monetare per kg

            // Cost suplimentar bazat pe categorie specială
            switch (CategorieSpeciala)
            {
                case "Fragil":
                    cost += 50;
                    break;
                case "Pretios":
                    cost += 100;
                    break;
                case "Periculos":
                    cost += 150;
                    break;
                default:
                    break;
            }

            return cost;
        }
    }
}