namespace Egui;

/// <inheritdoc cref="MutateDelegate{A,R}"/>
public delegate void MutateDelegate<A>(ref A value) where A : allows ref struct;

/// <summary>
/// A delegate for modifying a single value.
/// </summary>
/// <typeparam name="A">The type of the value to mutate.</typeparam>
/// <typeparam name="R">The return type.</typeparam>
/// <param name="value">The value to mutate.</param>
/// <returns>A result that will be passed to the outer function.</returns>
public delegate R MutateDelegate<A, R>(ref A value) where A : allows ref struct;