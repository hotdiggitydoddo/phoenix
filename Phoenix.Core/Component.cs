namespace Phoenix.Core
{
    public abstract class Component
    {
        public Entity Owner { get; set; }
        public Component()
        {
            
        }
    }

    public interface IUpdateable
    {
        void Update(double dt);
    }
}
