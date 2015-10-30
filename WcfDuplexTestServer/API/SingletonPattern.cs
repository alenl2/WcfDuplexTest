using System;

namespace WcfDuplexTest
{
    public abstract class SingletonPattern<T>
    {
        #region Constants and properties

        public static T Instance
        {
            get
            {
                return GetInstance();
            }
        }

        #endregion

        #region Private members

        private static readonly object s_InstanceLock = new object();
        private static T s_Instance;

        #endregion

        #region Constructors and destructors

        protected SingletonPattern()
        {
        }

        #endregion

        #region Public methods

        public static T GetInstance()
        {
            if (ReferenceEquals(s_Instance, null))
            {
                lock (s_InstanceLock)
                {
                    if (ReferenceEquals(s_Instance, null))
                    {
                        s_Instance = (T)Activator.CreateInstance(typeof(T), true);
                    }
                }
            }

            return s_Instance;
        }

        #endregion
    }
}
