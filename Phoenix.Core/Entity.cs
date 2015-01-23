﻿using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Core
{
    public class Entity
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        private List<Component> _components;

        public Entity()
        {
            Id = _nextId;
            _components = new List<Component>();
            _nextId++;
        }

        public bool HasComponent<T>() where T : Component
        {
            return _components.Exists(x => x.GetType() == typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            return _components.SingleOrDefault(x => x.GetType() == typeof(T)) as T;
        }

        public void AddComponent<T>(T component) where T : Component
        {
            _components.Add(component);
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }

        public void Update(double dt)
        {
            foreach (var component in _components)
                if (component is IUpdateable)
                    ((IUpdateable)component).Update(dt);
        }
    }
}
