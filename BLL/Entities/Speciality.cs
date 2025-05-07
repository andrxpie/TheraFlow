namespace BLL.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Specialists { get; set; }
    }
}
