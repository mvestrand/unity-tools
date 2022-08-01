using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using MVest.Unity.OdinInspector;
#endif

namespace MVest.Unity.Globals {
	/// <summary>
  /// A global variable of type T stored as a Unity ScriptableObject.
  /// </summary>
  /// <typeparam name="T"></typeparam>
	public interface IGlobalVariable<T> {
		/// <summary>
    /// Gets or sets the variable's value.
    /// </summary>
		T Value {
			get; 
			set;
		}

		/// <summary>
    /// Represents a method that will be called when a global variable's value is changed.
    /// </summary>
    /// <param name="variable">The variable that was changed.</param>
    /// <param name="oldValue">The old value of the variable.</param>
    public delegate void Listener(IGlobalVariable<T> variable, T oldValue);

    /// <summary>
    /// Add a listener to be called when the variable value changes.
    /// </summary>
    /// <param name="listener">Listener delegate to add.</param>
    void Register(Listener listener);
    /// <summary>
    /// Remove a listener from the variable.
    /// </summary>
    /// <param name="listener">Listener delegate to remove.</param>
		void Deregister(Listener listener);
	}

	/// <inheritdoc />
	public class GlobalVariable<T> : ScriptableObject, IGlobalVariable<T>{
		private int _listenerCallbackDepth = 0;
    private const int MAX_CALLBACK_DEPTH = 1; // Maximum nesting onChange listener callbacks before throwing an error
    private event IGlobalVariable<T>.Listener onChange;
		
		/// <summary>
    /// The value of the global variable.
    /// </summary>
		[SerializeField] private T _value;

		/// <inheritdoc />
		public T Value {
			get { return _value; }
			set { 
				T oldValue = _value;
				_value = value; 
				UpdateListeners(oldValue); }
		}

		public static implicit operator T(GlobalVariable<T> v) => v._value; // Automatic unboxing of variable value

		/// <inheritdoc />
		public void Register(IGlobalVariable<T>.Listener listener) {
			onChange += listener;
		}

		/// <inheritdoc />
		public void Deregister(IGlobalVariable<T>.Listener listener) {
			onChange -= listener;
		}

		private void UpdateListeners(T oldValue) {
			if (_listenerCallbackDepth > MAX_CALLBACK_DEPTH) {
				Debug.LogError("Cyclical set call of var \"" + name + "\": \n" + Environment.StackTrace);
				return;
			}
			_listenerCallbackDepth++;
      IGlobalVariable<T>.Listener listeners = onChange; // Store listener callbacks to prevent deregister calls while iterating from causing errors
      listeners?.Invoke(this, oldValue);
			_listenerCallbackDepth--;
		}
	}

	/// <summary>
  /// NOT IMPLEMENTED CORRECTLY. A readonly reference to a global variable. Ideally this could be used to limit where global variable changes can be coming from, however
  /// this implementation still allows for the variable value to be explicitly changed.
  /// </summary>
  /// <typeparam name="TVar">Global variable type</typeparam>
  /// <typeparam name="T">Variable type</typeparam>
	public interface IReadOnlyGlobalRef<TVar, T> where TVar : IGlobalVariable<T> {
		bool UseConstant { get; }
		T ConstantValue { get; }
		TVar Variable { get; }
		T Value { get; }
	}

	[Serializable]
	[DrawWithUnity]
	public class GlobalRef<TVar, T> : IReadOnlyGlobalRef<TVar, T> where TVar : IGlobalVariable<T> {
		[SerializeField] private bool useConstant = true;
		[SerializeField] private T constantValue;
		[SerializeField] private TVar variable;
		public bool UseConstant { get { return useConstant; } set { useConstant = value; } }
		public T ConstantValue { get { return constantValue; } set { constantValue = value; } }
		public TVar Variable { get { return variable; } set { variable = value; } }

		public T Value {
			get { return useConstant ? constantValue : variable.Value; }
			set { if (UseConstant) constantValue = value; else variable.Value = value; }
		}

		public static implicit operator T(GlobalRef<TVar, T> v) => v.Value;

	}

}
