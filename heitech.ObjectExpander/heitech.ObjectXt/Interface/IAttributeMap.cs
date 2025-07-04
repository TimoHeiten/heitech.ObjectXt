using System;
using heitech.ObjectXt.Interfaces;

namespace heitech.ObjectXt.Interface
{
    /// <summary>
    /// Map for attributes (add, has, canInvoke, call)
    /// </summary>
    internal interface IAttributeMap
    {
        /// <summary>
        /// Add an attribute to the object 
        /// </summary>
        /// <param name="extended"></param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <typeparam name="TKey"></typeparam>
        void Add<TKey>(object extended, TKey key, IExtensionAttribute func);

        /// <summary>
        /// Check if the attribute via the TKey is registered for the object
        /// </summary>
        /// <param name="extended"></param>
        /// <param name="key"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        bool HasKey<TKey>(object extended, TKey key);

        /// <summary>
        /// Can invoke for key and expectedReturnType
        /// </summary>
        /// <param name="extended"></param>
        /// <param name="key"></param>
        /// <param name="expectedReturnType"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        bool CanInvoke<TKey>(object extended, TKey key, Type expectedReturnType, params object[] parameters);
        /// <summary>
        /// actually invoke with key and specified parameters
        /// </summary>
        /// <param name="extended"></param>
        /// <param name="key"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        object Invoke<TKey>(object extended, TKey key, params object[] parameters);
    }
}
