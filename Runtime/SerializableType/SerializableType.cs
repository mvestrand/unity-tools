using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace MVest.Unity {

	/// <summary>
	/// A type reference that can be serialized by Unity and will show up in the inspector
	/// </summary>
	[Serializable]
	public struct SerializableType : ISerializationCallbackReceiver {

		// ====================================================================
		// This type dictionary is used to avoid using assembly qualified names
		private static Dictionary<string, Type> _typenameAliases;

		internal static Dictionary<string, Type> TypenameAliases { get { return GetTypenameTable(); } }

		/// <summary>
        /// Retrieve the typename table, creating it if necessary.
        /// </summary>
		private static Dictionary<string, Type> GetTypenameTable() {
			if (_typenameAliases != null)
				return _typenameAliases;

			// Create a new typename dictionary
			_typenameAliases = new Dictionary<string, Type>();

			// Cache the types explicitly flagged for qualified names
			EnsureNeedsQualifiedNameCached();

			// Build dictionary with common and tagged types
			var basicTypes = new Type[]{
				typeof(float),
				typeof(double),
				typeof(int),
				typeof(long),
				typeof(bool),
				typeof(string),
				typeof(Vector2),
				typeof(Vector2Int),
				typeof(Vector3),
				typeof(Vector3Int),
				typeof(Vector4),
				typeof(GameObject),
				typeof(UnityEngine.Object),
				typeof(UnityEngine.Component),
				typeof(UnityEngine.MonoBehaviour)}
				.Select(x => new Tuple<string, Type>(x.FullName, x));

			// Get all attribute tagged custom serializable types
			var customTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
				.Where(x => x.GetCustomAttributes(typeof(SerializableTypeAttribute), true).Length > 0)
				.Select(x => new Tuple<string, Type>(x.FullName, x));

			var allTypes = basicTypes.Concat(customTypes);


			// Add them to the typename dictionary
			foreach (var type in allTypes) {
				if (_typenameAliases.ContainsKey(type.Item1))
					Debug.LogError("Multiple definitions with type name \"" + type.Item1 + "\":\n --> " + type.Item2.AssemblyQualifiedName + "\n --> "
						+ _typenameAliases[type.Item1].AssemblyQualifiedName);
				_typenameAliases[type.Item1] = type.Item2;
			}

			return _typenameAliases;
		}

		/// <summary>
        /// Resolve a typename string to a Type.
        /// </summary>
		public static Type GetAliasType(string typename) {
			Type type;
			if (TypenameAliases.TryGetValue(typename, out type))
				return type;
			if (String.IsNullOrWhiteSpace(typename))
                return null;


            type = Type.GetType(typename);

			// Check assemblies for an unqualified typename
			if (type == null) {
				var foundTypes = AppDomain.CurrentDomain.GetAssemblies()
					.Select(x => x.GetType(typename))
					.Where(x => (x != null)).ToArray();

				// Type doesn't exist
				if (foundTypes.Length == 0)
					return null;

				// Found exactly one definition
				else if (foundTypes.Length == 1) {
					// Add to alias table and return
					Type foundType = foundTypes[0];
					TypenameAliases[typename] = foundType;
					return foundTypes[0];
				}
				// Found multiple definitions
				else {
					Debug.LogError("Multiple definitions of with typename \"" + typename + "\":\n --> "
					+ String.Join("\n --> ", foundTypes.Select(x => x.AssemblyQualifiedName)));
					return null;
				}
			}
			return type;

		}

		private static Dictionary<Type, bool> _needsQualifiedNameCached;

		/// <summary>
        /// Guarantee the dictionary of types that require qualified names is created.
        /// </summary>
		private static void EnsureNeedsQualifiedNameCached() {
			if (_needsQualifiedNameCached != null)
				return;
			_needsQualifiedNameCached = new Dictionary<Type, bool>();

			// Add all types flagged as requiring qualified names
			var flaggedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
				.Where(x => x.GetCustomAttributes(typeof(SerializableTypeAlwaysAssemblyQualifiedAttribute), false).Length > 0);
			foreach (var type in flaggedTypes) {
                _needsQualifiedNameCached[type] = true;
            }
			return;
		}

		/// <summary>
        /// Check if the given type requires an assembly qualified name.
        /// </summary>
		public static bool NeedsAssemblyQualifiedName(Type type) {
			EnsureNeedsQualifiedNameCached();
			bool ans;
			if (_needsQualifiedNameCached.TryGetValue(type, out ans))
				return ans;
			// Flagged typenames don't need to be qualified
			else if (TypenameAliases.ContainsKey(type.FullName)) {
				_needsQualifiedNameCached[type] = false;
				return false;
			}

			_needsQualifiedNameCached[type] = AppDomain.CurrentDomain.GetAssemblies()
					.Select(x => x.GetType(type.FullName)).Count() > 1;
			return _needsQualifiedNameCached[type];
		}

		// Static constructor
		static SerializableType() {
			GetTypenameTable();
		}


		//==================================================================

		public static implicit operator Type(SerializableType t) => t.Value;
		public static implicit operator SerializableType(Type t) => new SerializableType(t);

		private Type _type;
		[SerializeField] private string _typename;

		public Type Value { get { return _type; } set { _type = value; dirty = true; } }
		public string Typename { get { UpdateTypename(); return _typename; } }
		private bool dirty;

		public SerializableType(Type type) {
			_type = type;
			_typename = null;
			dirty = true;
		}

		public void OnBeforeSerialize() {
			UpdateTypename();
		}

		public void OnAfterDeserialize() {
			if (_typename.Length > 0)
				_type = GetAliasType(_typename);
			else
				_type = null;
			dirty = false;
		}

		private void UpdateTypename() {
			if (dirty || _typename == null) {
				if (_type == null)
					_typename = "";
				else if (NeedsAssemblyQualifiedName(_type))
					_typename = _type.AssemblyQualifiedName;
				else
					_typename = _type.FullName;
				dirty = false;
			}
		}
	}

}
