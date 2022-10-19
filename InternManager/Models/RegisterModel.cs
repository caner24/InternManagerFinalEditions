using InternManager.Entities.Concrate;

namespace InternManager.WebUI.Models
{
    public class RegisterModel
    {
        public Person Persons { get; set; }
        public Student  Students { get; set; }

        public Faculty Fakülte { get; set; }
    }
}
