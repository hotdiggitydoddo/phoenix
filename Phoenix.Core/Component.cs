namespace Phoenix.Core
{
    public abstract class Component
    {
        public Entity Owner { get; set; }

        public virtual bool OkMessage(Message message) { return true; }
    }

    public interface IUpdateable
    {
        void Update(double dt);
    }
}
