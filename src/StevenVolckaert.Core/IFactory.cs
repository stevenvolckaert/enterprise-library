namespace StevenVolckaert
{
    /// <summary>
    ///     Represents a factory that requires no parameters to create instances of the specified type.
    /// </summary>
    /// <typeparam name="TResult">
    ///     The type of the instances that this factory creates.
    /// </typeparam>
    public interface IFactory<out TResult>
    {
        /// <summary>
        ///     Creates a new instance of the <typeparamref name="TResult"/> type.
        /// </summary>
        /// <returns>
        ///     A new instance of the <typeparamref name="TResult"/> type.
        /// </returns>
        TResult CreateInstance();
    }

    /// <summary>
    ///     Represents a factory that requires one parameter to create instances of the specified type.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the parameter required to create an instance of
    ///     <typeparamref name="TResult"/>.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///     The type of the instances that this factory creates.
    /// </typeparam>
    public interface IFactory<in T, out TResult>
    {
        /// <summary>
        ///     Creates a new instance of the <typeparamref name="TResult"/> type.
        /// </summary>
        /// <param name="arg">
        ///     The parameter required to create an instance.
        /// </param>
        /// <returns>
        ///     A new instance of the <typeparamref name="TResult"/> type.
        /// </returns>
        TResult CreateInstance(T arg);
    }

    /// <summary>
    ///     Represents a factory that requires two parameters to create instances of the specified type.
    /// </summary>
    /// <typeparam name="T1">
    ///     The type of the first parameter required to create an instance of
    ///     <typeparamref name="TResult"/>.
    /// </typeparam>
    /// <typeparam name="T2">
    ///     The type of the second parameter required to create an instance of
    ///     <typeparamref name="TResult"/>.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///     The type this factory creates instances of.
    /// </typeparam>
    public interface IFactory<in T1, in T2, out TResult>
    {
        /// <summary>
        ///     Creates a new instance of the <typeparamref name="TResult"/> type.
        /// </summary>
        /// <param name="arg1">
        ///     The first parameter required to create an instance.
        /// </param>
        /// <param name="arg2">
        ///     The second parameter required to create an instance.
        /// </param>
        /// <returns>
        ///     A new instance of the <typeparamref name="TResult"/> type.
        /// </returns>
        TResult CreateInstance(T1 arg1, T2 arg2);
    }
}
