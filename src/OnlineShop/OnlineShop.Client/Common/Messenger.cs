using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Common
{
    class Messenger
    {
        private readonly Dictionary<MessengerKey, object> dictionary = new Dictionary<MessengerKey, object>();

        protected Messenger()
        {

        }

        private static Messenger instance;

        public static Messenger Default
        {
            get
            {
                if (instance == null)
                    instance = new Messenger();
                return instance;
            }
        }

        public void Register<TKey, T>(object recipient, TKey key, Action<T> action)
        {
            if (recipient == null)
            {
                throw new ArgumentNullException("recipient");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            dictionary.Add(new MessengerKey(recipient, key), action);
        }

        public void Unregister<T>(object recipient, T key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            dictionary.Remove(new MessengerKey(recipient, key));
        }

        public void Send<TKey, T>(TKey key, T message)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            IEnumerable<KeyValuePair<MessengerKey, object>> result = dictionary.Where(keyValuePair => keyValuePair.Key.Key.Equals(key));

            foreach (var action in result.Select(x => x.Value).OfType<Action<T>>())
            {
                action(message);
            }
        }

        private class MessengerKey
        {
            public object Recipient { get; private set; }
            public object Key { get; private set; }

            public MessengerKey(object recipient, object key)
            {
                Recipient = recipient;
                Key = key;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;

                MessengerKey messageKey = obj as MessengerKey;

                return this.Key.Equals(messageKey.Key) && this.Recipient.Equals(messageKey.Recipient);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Recipient != null ? Recipient.GetHashCode() : 0) * 397) ^ (Key != null ? Key.GetHashCode() : 0);
                }
            }
        }
    }
}
