using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Core
{
    public abstract class World
    {
        protected List<Entity> _entities;
        protected List<Component> _componentsToAdd;
        protected List<Component> _componentsToRemove;
        protected List<Entity> _entitiesToAdd;
        protected List<Entity> _entitiesToRemove;
        protected List<Module> _modules;
 
        public World()
        {
            _entities = new List<Entity>();
            _entitiesToAdd = new List<Entity>();
            _entitiesToRemove = new List<Entity>();
            _componentsToAdd = new List<Component>();
            _componentsToRemove = new List<Component>();
            _modules = new List<Module>();
        }

        public void AddEntityToWorld(Entity entity, params Component[] components)
        {
            foreach (var component in components)
            {
                component.Owner = entity;
                _componentsToAdd.Add(component);
            }
            _entitiesToAdd.Add(entity);
           
        }
        public void RemoveEntityFromWorld(Entity entity)
        {
            _entitiesToRemove.Add(entity);
            var message = new Message(entity.Id, 0, 0, "REMOVED_ENTITY");
            PostOffice.Instance.AddMessage(message);
        }

        public Entity GetEntity(int id)
        {
            return _entities.SingleOrDefault(x => x.Id == id);
        }

        public void AddComponentToEntity<T>(int id, T component) where T : Component
        {
            var entity = _entities.SingleOrDefault(x => x.Id == id);

            if (!entity.HasComponent<T>())
            {
                component.Owner = entity;
                _componentsToAdd.Add(component);
            }
        }

        public void RemoveComponentFromEntity<T>(int id) where T : Component
        {
            var entity = _entities.SingleOrDefault(x => x.Id == id);
            if (entity.HasComponent<T>())
            {
                var component = entity.GetComponent<T>();
                _componentsToRemove.Add(component);
            }
        }

        protected void Update(double dt)
        {
            PostOffice.Instance.DispatchMessages();
         
            foreach (var entity in _entities)
                entity.Update(dt);

            foreach (var entity in _entitiesToAdd)
                _entities.Add(entity);
            foreach (var entity in _entitiesToRemove)
                _entities.Remove(entity);
            foreach (var component in _componentsToAdd)
                if (_entities.Contains(component.Owner))
                    _entities.Single(x => x == component.Owner).AddComponent(component);
            foreach (var component in _componentsToRemove)
                if (_entities.Contains(component.Owner))
                    _entities.Single(x => x == component.Owner).RemoveComponent(component);

            foreach (var entity in _entitiesToAdd)
            {
                var message = new Message(entity.Id, 0, 0, "ADDED_ENTITY");
                PostOffice.Instance.AddMessage(message);
            }

            foreach (var component in _componentsToAdd)
            {
                var message = new Message(component.Owner.Id, 0, 0, "ADDED_COMPONENT", 0, false, component.GetType().ToString());
                PostOffice.Instance.AddMessage(message);
            }

            foreach (var entity in _entitiesToRemove)
            {
                var message = new Message(entity.Id, 0, 0, "REMOVED_ENTITY");
                PostOffice.Instance.AddMessage(message);
            }

            foreach (var component in _componentsToRemove)
            {
                var message = new Message(component.Owner.Id, 0, 0, "REMOVED_COMPONENT", 0, false, component.GetType().ToString());
                PostOffice.Instance.AddMessage(message);
            }


            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();
            _componentsToAdd.Clear();
            _componentsToRemove.Clear();
        }
    }
}
