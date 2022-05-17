using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Thunisoft.Framework.Utilities
{
	#region Enumerations

	public enum BooleanPropertyValues
	{
		BooleanTrue,
		BooleanFalse,
		NullableBooleanTrue,
		NullableBooleanFalse,
		NullableBooleanNull,
	}

	#endregion

	#region ResourceInfo Class

	public class ResourceInfo
	{
		#region Protected Member Variables

		// ******************************************************************
		// *																*
		// *					Protected Member Variables			        *
		// *																*
		// ******************************************************************

		// Protected member variables
		protected Assembly m_assembly;
		protected bool m_isResource;
		protected string m_resourceName;
		protected string m_resourceManifestName;
		protected object m_resourceValue;
		protected bool m_isManifest;
		protected string m_manifestName;
		protected Stream m_manifestStream;

		#endregion

		#region Constructors

		// ******************************************************************
		// *																*
		// *					      Constructors					        *
		// *																*
		// ******************************************************************

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="anAssembly">
		/// An Assembly that contains the resource
		/// </param>
		/// <param name="aResourceValue">
		/// An object reference to the resource instance
		/// </param>
		/// <param name="aResourceName">
		/// A string that specifies the resource name
		/// </param>
		/// <param name="aResourceManifestName">
		/// A string that specifies the manifest resource name
		/// </param>
		public ResourceInfo(
			Assembly anAssembly,
			object aResourceValue,
			string aResourceName,
			string aResourceManifestName)
		{
			// Set members to default
			m_assembly = anAssembly;
			m_isResource = true;
			m_resourceName = aResourceName;
			m_resourceManifestName = aResourceManifestName;
			m_resourceValue = aResourceValue;
			m_isManifest = false;
			m_manifestStream = null;
			m_manifestName = string.Empty;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="anAssembly">
		/// An Assembly that contains the resource
		/// </param>
		/// <param name="aManifestName">
		/// A string that specifies the manifest resource name
		/// </param>
		public ResourceInfo(
			Assembly anAssembly,
			string aManifestName)
		{
			// Set members to default
			m_assembly = anAssembly;
			m_isManifest = true;
			m_manifestName = aManifestName;
			m_manifestStream = null;
			m_isResource = false;
			m_resourceName = string.Empty;
			m_resourceManifestName = string.Empty;
			m_resourceValue = null;
		}

		#endregion

		#region Finalizer

		// ******************************************************************
		// *																*
		// *					        Finalizer					        *
		// *																*
		// ******************************************************************

		/// <summary>
		/// Finalizer
		/// </summary>
		~ResourceInfo()
		{
			if (m_manifestStream != null)
			{
				m_manifestStream.Dispose();
				m_manifestStream = null;
			}
		}

		#endregion

		#region Public Properties

		// ******************************************************************
		// *																*
		// *					     Public Properties				        *
		// *																*
		// ******************************************************************

		/// <summary>
		/// Gets an Assembly reference that contains the resource
		/// </summary>
		public virtual Assembly Assembly
		{
			get { return m_assembly; }
		}

		/// <summary>
		/// Gets a bool that indicates if the resource is embedded
		/// as a manifest resource stream into the assembly
		/// </summary>
		public virtual bool IsManifest
		{
			get { return m_isManifest; }
		}

		/// <summary>
		/// Gets a bool that indicates if the resource is embedded as
		/// a resource value into the assembly
		/// </summary>
		public virtual bool IsResource
		{
			get { return m_isResource; }
		}

		/// <summary>
		/// Gets a Stream reference to the embedded manifest resource
		/// </summary>
		public virtual Stream ManifestStream
		{
			get
			{
				// Do we cache the stream already
				if (m_isManifest &&
					m_manifestStream == null)
				{
					m_manifestStream = m_assembly.GetManifestResourceStream(
						m_manifestName);
				}

				// Return stream
				return m_manifestStream;
			}
		}

		/// <summary>
		/// Gets a string that specifies the fully qualified manifest 
		/// resource name
		/// </summary>
		public virtual string ManifestName
		{
			get { return m_manifestName; }
		}

		/// <summary>
		/// Gets an object reference to the embedded resource value
		/// </summary>
		public virtual object ResourceValue
		{
			get { return m_resourceValue; }
		}

		/// <summary>
		/// Gets a string that specifies the resource name
		/// </summary>
		public virtual string ResourceName
		{
			get { return m_resourceName; }
		}

		/// <summary>
		/// Gets a string that specifies the fully qualified manifest 
		/// resource name
		/// </summary>
		public virtual string ResourceManifestName
		{
			get { return m_resourceManifestName; }
		}

		#endregion
	}

	#endregion

	#region ReflectionUtil Class

	// ******************************************************************
	// *																*
	// *					   ReflectionUtil Class		                *
	// *																*
	// ******************************************************************

	/// <summary>
	/// A static class that contains helper functions for <i>Type</i>
	/// and <i>Attribute</i> resolving and generic reflection functions 
	/// </summary>
	public static class ReflectionUtil
	{
		#region Private Member Variables

		// ******************************************************************
		// *																*
		// *				     Private member variables				    *
		// *																*
		// ******************************************************************

		// Private static member variables
		private static Dictionary<Type, string>
			s_dicValidateInstanceType;
		private static Dictionary<Type, Dictionary<string, PropertyInfo>>
			s_dicPropertyInfos;

		#endregion

		#region Constructors

		// ******************************************************************
		// *																*
		// *						 Constructors							*
		// *																*
		// ******************************************************************

		/// <summary>
		/// Static Constructor
		/// </summary>
		static ReflectionUtil()
		{
			// Set static members to default
			s_dicValidateInstanceType
				= new Dictionary<Type, string>();
			s_dicPropertyInfos
				= new Dictionary<Type, Dictionary<string, PropertyInfo>>();
		}

		#endregion

		#region Private Methods

		// ******************************************************************
		// *																*
		// *						Private Methods							*
		// *																*
		// ******************************************************************


		#endregion

		#region Public Methods
		public static bool SetPropertyAttributeReadOnly(
			Type aClassType,
			string aPropertyName,
			bool aReadOnlyFlag,
			bool aThrowExceptionOnFail)
		{
			// Try to get descriptor of specified property
			PropertyDescriptor prop = TypeDescriptor.GetProperties(aClassType)[aPropertyName];
			if (prop == null)
			{
				// Do we throw an exception?
				if (aThrowExceptionOnFail)
					throw new InvalidOperationException(string.Format(
						"Type '{0}' does not contain a property with name '{1}'",
						aClassType.FullName,
						aPropertyName));
				return false;
			}

			// Find ReadOnly attribute
			ReadOnlyAttribute attr = null;
			for (int i = 0; i < prop.Attributes.Count; i++)
			{
				// Try to get ReadOnly reference
				attr = prop.Attributes[i] as ReadOnlyAttribute;
				if (attr != null)
					break;
			}

			// Check if we found the required attribute
			if (attr == null)
			{
				// Do we throw an exception
				if (aThrowExceptionOnFail)
					throw new InvalidOperationException(string.Format(
						"Property '{0}' does not have a ReadOnlyAttribute assigned",
						aPropertyName));
				return false;
			}

			// Get reference to private field
			FieldInfo field = attr.GetType().GetField(
				"isReadOnly",
				BindingFlags.NonPublic |
				BindingFlags.Instance);

			// Check if value must be changed
			bool isReadOnly = (bool)field.GetValue(attr);
			if (isReadOnly != aReadOnlyFlag)
				field.SetValue(attr, aReadOnlyFlag);

			// Return success
			return true;
		}
		public static bool SetPropertyAttributeDisplayName(
			Type aClassType,
			string aPropertyName,
			string aDisplayName,
			bool aThrowExceptionOnFail)
		{
			// Try to get descriptor of specified property
			PropertyDescriptor prop = TypeDescriptor.GetProperties(aClassType)[aPropertyName];
			if (prop == null)
			{
				// Do we throw an exception?
				if (aThrowExceptionOnFail)
					throw new InvalidOperationException(string.Format(
						"Type '{0}' does not contain a property with name '{1}'",
						aClassType.FullName,
						aPropertyName));
				return false;
			}

			// Find DisplayName attribute
			DisplayNameAttribute attr = null;
			for (int i = 0; i < prop.Attributes.Count; i++)
			{
				// Try to get ReadOnly reference
				attr = prop.Attributes[i] as DisplayNameAttribute;
				if (attr != null)
					break;
			}

			// Check if we found the required attribute
			if (attr == null)
			{
				// Do we throw an exception
				if (aThrowExceptionOnFail)
					throw new InvalidOperationException(string.Format(
						"Property '{0}' does not have a DisplayName assigned",
						aPropertyName));
				return false;
			}

			// Get reference to private field
			FieldInfo field = attr.GetType().GetField(
				"_displayName",
				BindingFlags.NonPublic |
				BindingFlags.Instance);

			// Check if value must be changed
			string displayName = (string)field.GetValue(attr);
			if (displayName != aDisplayName)
				field.SetValue(attr, aDisplayName ?? string.Empty);

			// Return success
			return true;
		}
		public static bool TryGetPropertyAttributeReadOnly(
			Type aClassType,
			string aPropertyName,
			out bool aReadOnlyFlag)
		{
			// Set default value
			aReadOnlyFlag = false;

			// Try to get descriptor of specified property
			PropertyDescriptor prop = TypeDescriptor.GetProperties(aClassType)[aPropertyName];
			if (prop == null)
				return false;

			// Find ReadOnly attribute
			ReadOnlyAttribute attr = null;
			for (int i = 0; i < prop.Attributes.Count; i++)
			{
				// Try to get ReadOnly reference
				attr = prop.Attributes[i] as ReadOnlyAttribute;
				if (attr != null)
					break;
			}

			// Check if we found the required attribute
			if (attr == null)
				return false;

			// Get ReadOnly value and return success
			aReadOnlyFlag = attr.IsReadOnly;
			return true;
		}


		public static bool TryGetPropertyValue<T>(
			object anInstance,
			string aPropertyName,
			out T aPropertyValue)
		{
			// Set default return value
			aPropertyValue = default(T);

			try
			{
				// Try to get the actual property value
				aPropertyValue = GetPropertyValue<T>(
					anInstance,
					aPropertyName);
				return true;
			}
			catch (Exception)
			{
				// Do not handle exception, but return error code
				return false;
			}
		}

		public static bool TrySetPropertyValue<T>(
			object anInstance,
			string aPropertyName,
			T aPropertyValue)
		{
			try
			{
				// Try to set the actual property value
				SetPropertyValue<T>(
					anInstance,
					aPropertyName,
					aPropertyValue);
				return true;
			}
			catch (Exception)
			{
				// Do not handle excpetion but return error code
				return false;
			}
		}
		public static T GetPropertyValue<T>(
			object anInstance,
			string aPropertyName)
		{
			// Check input
			if (anInstance == null)
				throw new ArgumentException(
					"Invalid object reference; <null> not allowed");

			// Declare variables
			Type type = anInstance.GetType();
			PropertyInfo[] props = GetPropertyInfos(type, aPropertyName);

			// Check if a single PropertyInfo could be resolved
			if (props.Length != 1)
			{
				// Throw exception because property could not be resolved
				throw new ArgumentException(string.Format(
					"Invalid PropertyName specified; type '{0}' does not contain property '{1}'",
					type.FullName,
					aPropertyName));
			}

			// Check if value of property can be read
			if (!props[0].CanRead)
			{
				// Throw exception because property has no getter
				throw new ArgumentException(string.Format(
					"Invalid PropertyName specified; property '{0}' on type '{1}' cannot be read",
					aPropertyName,
					type.FullName));
			}

			// Check if type of property matches specified one
			if (props[0].PropertyType != typeof(T))
			{
				// Throw excpetion because property type does not match
				throw new ArgumentException(string.Format(
					"Invalid Property type specified; property '{0} ({1})' on type '{2}' does " +
					"not match with requested type '{3}'",
					aPropertyName,
					props[0].PropertyType.Name,
					type.FullName,
					typeof(T).Name));
			}

			// Finally try to get the actual property value from the instance
			return (T)props[0].GetValue(anInstance, null);
		}

		public static void SetPropertyValue<T>(
			object anInstance,
			string aPropertyName,
			T aPropertyValue)
		{
			// Check input
			if (anInstance == null)
				throw new ArgumentException(
					"Invalid object reference; <null> not allowed");

			// Declare variables
			Type type = anInstance.GetType();
			PropertyInfo[] props = GetPropertyInfos(type, aPropertyName);

			// Check if a single PropertyInfo could be resolved
			if (props.Length != 1)
			{
				// Throw exception because property could not be resolved
				throw new ArgumentException(string.Format(
					"Invalid PropertyName specified; type '{0}' does not contain property '{1}'",
					type.FullName,
					aPropertyName));
			}

			// Check if value of property can be written
			if (!props[0].CanWrite)
			{
				// Throw exception because property has no getter
				throw new ArgumentException(string.Format(
					"Invalid PropertyName specified; property '{0}' on type '{1}' cannot be written",
					aPropertyName,
					type.FullName));
			}

			// Check if type of property matches specified one
			if (props[0].PropertyType != typeof(T))
			{
				// Throw excpetion because property type does not match
				throw new ArgumentException(string.Format(
					"Invalid Property type specified; property '{0} ({1})' on type '{2}' does " +
					"not match with requested type '{3}'",
					aPropertyName,
					props[0].PropertyType.Name,
					type.FullName,
					typeof(T).Name));
			}

			// Finally try to set the actual property value onto the instance
			props[0].SetValue(anInstance, aPropertyValue, null);
		}

		public static bool IsDelegate(Type aType)
		{
			// Check input
			if (aType != null)
			{
				// Check if specified Type is derived from Delegate
				return aType.IsSubclassOf(typeof(Delegate));
			}
			else
			{
				// Not a Delegate type
				return false;
			}
		}

		public static PropertyInfo[] GetPropertyInfos(
			Type anInterfaceType,
			bool anIncludeInherited)
		{
			// Check input
			if (anInterfaceType == null)
				throw new ArgumentException(
					"Invalid InterfaceType specified; <null> not allowed!");
			if (!anInterfaceType.IsInterface)
				throw new ArgumentException(
					string.Format(
						"Invalid InterfaceType specified; specified type '{0}' is not an interface!",
						anInterfaceType.Name));

			// Declare variables
			List<Type> listInterfaces;
			Dictionary<string, PropertyInfo> listProperties;
			PropertyInfo[] properties;

			// Build list of interface properties
			listProperties = new Dictionary<string, PropertyInfo>();
			listInterfaces = new List<Type>(new Type[] { anInterfaceType });

			// Include inherited properties
			if (anIncludeInherited)
				listInterfaces.AddRange(anInterfaceType.GetInterfaces());

			// Loop interfaces and append properties to list
			foreach (Type interfaceType in listInterfaces)
			{
				// Get properties from interface 
				properties = GetPropertyInfosFromType(interfaceType, false);

				// Append new properties to list
				foreach (PropertyInfo property in properties)
					if (!listProperties.ContainsKey(property.Name))
						listProperties.Add(property.Name, property);
			}

			// Convert list into array
			int i = 0;
			properties = new PropertyInfo[listProperties.Values.Count];
			foreach (PropertyInfo property in listProperties.Values)
				properties[i++] = property;

			// Return the array of properties
			return properties;
		}

		public static PropertyInfo[] GetPropertyInfos(
			object anInstance,
			bool anIncludeInherited)
		{
			// Check if valid object reference is specified
			if (anInstance == null)
				throw new ArgumentException(
					"Invalid Instance specified; <null> not allowed!");

			// Delegate call
			return GetPropertyInfosFromType(
				anInstance.GetType(),
				anIncludeInherited);
		}

		public static PropertyInfo[] GetPropertyInfosFromType(
			Type aType,
			bool anIncludeInherited)
		{
			// Check if valid object reference is specified
			if (aType == null)
				throw new ArgumentException(
					"Invalid Type specified; <null> not allowed!");

			// Return array of properties
			return aType.GetProperties(anIncludeInherited ?
				BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy :
				BindingFlags.Public | BindingFlags.Instance);
		}


		public static Stream GetResourceStream(
			Assembly anAssembly,
			string aResourceName,
			bool aCaseSensitive)
		{
			// Check input
			if (anAssembly == null)
				return null;
			if (string.IsNullOrEmpty(aResourceName))
				return null;

			// Try to load the bitmap
			Stream stream = anAssembly.GetManifestResourceStream(aResourceName);
			if (stream == null && !aCaseSensitive)
			{
				// Adjust input string
				string resourceName = aResourceName.Trim().ToLower();

				// Scan resource names
				foreach (string name in anAssembly.GetManifestResourceNames())
				{
					// Check if name matches
					string checkName = name.Trim().ToLower();
					if (checkName == resourceName)
					{
						// Try to load nearest match
						stream = anAssembly.GetManifestResourceStream(name);
						break;
					}
				}
			}

			// Return stream
			return stream;
		}
		public static bool HasPublicProperty(
			Type aClassType,
			string aPropertyName)
		{
			// Find property on class type
			PropertyInfo propertyInfo = aClassType.GetProperty(aPropertyName);
			if (propertyInfo == null)
				return false;

			// Check if property has getter
			if (!propertyInfo.CanRead)
				return false;

			// If we reach this line, assume class has specified public property
			return true;
		}

		public static Tattrib[] GetAttributes<Tattrib>(
			Type aType,
			bool anInheritFlag)
			where Tattrib : Attribute
		{
			// Check input
			return aType != null ?
				(Tattrib[])aType.GetCustomAttributes(typeof(Tattrib), anInheritFlag) :
				new Tattrib[0];
		}

		public static Tattrib GetAttribute<Tattrib>(
			Type aType,
			bool anInheritFlag)
			where Tattrib : Attribute
		{
			// Declare variables
			Tattrib attr = default(Tattrib);

			// Check input
			if (aType != null)
			{
				// Try to find specified attribyte
				object[] obj = aType.GetCustomAttributes(
					typeof(Tattrib),
					anInheritFlag);
				if (obj.Length > 0)
					attr = (Tattrib)obj[0];
			}

			// Return attribyte reference
			return attr;
		}

		public static Tattrib GetAttribute<Tattrib>(
			MethodInfo aMethod,
			bool anInheritFlag)
			where Tattrib : Attribute
		{
			// Declare variables
			Tattrib attr = default(Tattrib);

			// Check input
			if (aMethod != null)
			{
				// Try to find specified attribyte
				object[] obj = aMethod.GetCustomAttributes(
					typeof(Tattrib),
					anInheritFlag);
				if (obj.Length > 0)
					attr = (Tattrib)obj[0];
			}

			// Return attribyte reference
			return attr;
		}

		public static Tattrib GetAttribute<Tattrib>(
			PropertyInfo aProperty,
			bool anInheritFlag)
			where Tattrib : Attribute
		{
			// Declare variables
			Tattrib attr = default(Tattrib);

			// Check input
			if (aProperty != null)
			{
				// Try to find specified attribyte
				object[] obj = aProperty.GetCustomAttributes(
					typeof(Tattrib),
					anInheritFlag);
				if (obj.Length > 0)
					attr = (Tattrib)obj[0];
			}

			// Return attribyte reference
			return attr;
		}

        public static Tattrib GetAttribute<Tattrib>(
    FieldInfo aFieldInfo,
    bool anInheritFlag)
    where Tattrib : Attribute
        {
            // Declare variables
            Tattrib attr = default(Tattrib);

            // Check input
            if (aFieldInfo != null)
            {
                // Try to find specified attribute
                object[] obj = aFieldInfo.GetCustomAttributes(
                    typeof(Tattrib),
                    anInheritFlag);
                if (obj.Length > 0)
                    attr = (Tattrib)obj[0];
            }

            // Return attribute reference
            return attr;
        }

        public static bool HasAttribute<Tattrib>(Type aType)
			where Tattrib : Attribute
		{
			return HasAttribute<Tattrib>(aType, false);
		}

		public static bool HasAttribute<Tattrib>(Type aType, bool anInheritFlag)
			where Tattrib : Attribute
		{
			return GetAttribute<Tattrib>(aType, anInheritFlag) != null;
		}
		public static bool IsImplementationOf(
			this Type aType,
			Type anInterfaceType)
		{
			return aType.IsInterface ?
				aType == anInterfaceType ||
				aType.IsSubclassOf(anInterfaceType) :
				aType.GetInterfaces().Any(anInterfaceType.Equals);
		}

		public static Tinterface CreateInstance<Tinterface>(
			Type anInstanceType)
			where Tinterface : class
		{
			// Validate the specified interface
			ValidateInterfaceOnInstanceType(
				anInstanceType,
				typeof(Tinterface).Name,
				true);

			// Create new instance
			return (Tinterface)Activator.CreateInstance(anInstanceType);
		}
		public static void ValidateInterfaceOnInstanceType(
			Type anInstanceType,
			string anInterfaceName,
			bool aCheckForParameterlessConstructor)
		{
			// Check input
			if (anInstanceType == null)
				throw new ArgumentException(
					"Invalid InstanceType specified; <null> not allowed");
			if (string.IsNullOrEmpty(anInterfaceName))
				throw new ArgumentException(
					"Invalid InterfaceName specified; <null> or <empty> not allowed");

			// Declare variables
			string exceptionMsg = string.Empty;

			// Check if type is already validates
			if (!s_dicValidateInstanceType.TryGetValue(anInstanceType, out exceptionMsg))
			{
				try
				{
					// Check if specified interface is implemented on instance type
					Type typeInterface = anInstanceType.GetInterface(
						anInterfaceName,
						true);
					if (typeInterface == null)
					{
						// Could not find specified interface
						exceptionMsg = string.Format(
							"Interface '{0}' not implemented on type '{1}'",
							anInterfaceName,
							anInstanceType.Name);
					}
				}
				catch (Exception ex)
				{
					// Exception occured during interface resolving
					exceptionMsg = string.Format(
						"{0} occured on executing '{1}.GetInterface( {3} )'\n" +
						"Exception message:\n" +
						"{2}",
						ex.GetType().Name,
						anInstanceType.GetType().FullName,
						ex.Message,
						anInterfaceName);
				}

				// Check if we need to process
				if (string.IsNullOrEmpty(exceptionMsg) &&
					aCheckForParameterlessConstructor)
				{
					try
					{
						// Check if instance type has a public parameterless constructor
						ConstructorInfo ctor = anInstanceType.GetConstructor(new Type[0]);
						if (ctor == null)
						{
							// Could not find specified constructor
							exceptionMsg = string.Format(
								"No public parameterless constructor implemented on type '{0}'",
								anInstanceType.FullName);
						}
					}
					catch (Exception ex)
					{
						// Exception occured during constructor resolving
						exceptionMsg = string.Format(
							"{0} occured on executing '{1}.GetConstructor( Type.EmptyTypes )'\n" +
							"Exception message:\n" +
							"{2}",
							ex.GetType().Name,
							anInstanceType.GetType().FullName,
							ex.Message);
					}
				}

				// Append message to dictionary
				s_dicValidateInstanceType.Add(anInstanceType, exceptionMsg);
			}

			// Check specified is valid or not
			if (!string.IsNullOrEmpty(exceptionMsg))
			{
				// Specified type is not valid so we need to raise an exception
				throw new ArgumentException(exceptionMsg);
			}
		}

		public static PropertyInfo[] GetPropertyInfos(
			Type aType,
			params string[] aPropertyNames)
		{
			// Check input argument
			if (aPropertyNames == null ||
				aPropertyNames.Length == 0)
			{
				// Return empty collection
				return new PropertyInfo[0];
			}

			// Declare variables
			List<PropertyInfo> list = new List<PropertyInfo>();
			PropertyInfo property = null;

			// Check if we already cache the properties of this type
			Dictionary<string, PropertyInfo> dictionary;
			if (!s_dicPropertyInfos.TryGetValue(aType, out  dictionary))
			{
				// Get PropertyInfo objects
				PropertyInfo[] props = GetPropertyInfosFromType(aType, true);

				// Build new dictionary with all property infos
				dictionary = new Dictionary<string, PropertyInfo>(
					StringComparer.InvariantCultureIgnoreCase);
				foreach (PropertyInfo propertyInfo in props)
					dictionary[propertyInfo.Name] = propertyInfo;

				// Register entire set with cache
				s_dicPropertyInfos[aType] = dictionary;
			}

			// Loop property names and find mapping infos
			foreach (string propertyName in aPropertyNames)
			{
				// Try to find associated property
				if (!dictionary.TryGetValue(propertyName, out property))
				{
					// Unsupported propertyname encountered
					throw new ArgumentException(string.Format(
						"Invalid PropertyName specified; property '{0}' does not exist on type '{1}'.",
						propertyName,
						aType.FullName));
				}

				// Add property to list
				list.Add(property);
			}

			// Return list as array
			return list.ToArray();
		}

		public static bool Equals(object anInstance, int aHashCode)
		{
			// Compare hashcodes
			return anInstance != null ?
				anInstance.GetHashCode() == aHashCode :
				false;
		}

		public static bool IsNullable(BooleanPropertyValues aValue)
		{
			switch (aValue)
			{
				case BooleanPropertyValues.NullableBooleanNull:
				case BooleanPropertyValues.NullableBooleanFalse:
				case BooleanPropertyValues.NullableBooleanTrue:
					return true;
				default:
					return false;
			}
		}

		public static bool IsNullable(Type aType)
		{
			return Nullable.GetUnderlyingType(aType) != null;
		}

		public static BooleanPropertyValues InvertBooleanPropertyValue(
			BooleanPropertyValues aValue,
			BooleanPropertyValues anInvertValueForNullableBooleanNull)
		{
			switch (aValue)
			{
				case BooleanPropertyValues.BooleanFalse:
					return BooleanPropertyValues.BooleanTrue;
				case BooleanPropertyValues.BooleanTrue:
					return BooleanPropertyValues.BooleanFalse;
				case BooleanPropertyValues.NullableBooleanFalse:
					return BooleanPropertyValues.NullableBooleanTrue;
				case BooleanPropertyValues.NullableBooleanTrue:
					return BooleanPropertyValues.NullableBooleanFalse;
				case BooleanPropertyValues.NullableBooleanNull:
					return anInvertValueForNullableBooleanNull;
				default:
					throw new InvalidOperationException();
			}
		}

        public static TAttrib GetEnumAttribute<TAttrib>(Enum anEnum) where TAttrib:Attribute
        {
            string enumName = Enum.GetName(anEnum.GetType(), anEnum);
            FieldInfo field = anEnum.GetType().GetField(enumName);
            return GetAttribute<TAttrib>(field, false);
        }
        public static TChild AutoCopy<TParent, TChild>(TParent parent) where TChild : TParent, new()
        {
            TChild child = new TChild();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;
        }
        #endregion
    }

	#endregion
}
