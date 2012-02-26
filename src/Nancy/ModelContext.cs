namespace Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Reflection;
    using Nancy.Extensions;
    using Nancy.Validation;

    /// <summary>
    /// A context for the model used to render the view.
    /// </summary>
    public class ModelContext : DynamicObject, IHideObjectMembers
    {
        private ModelValidationResult validationResult;

        /// <summary>
        /// Gets the model.
        /// </summary>
        public object Model { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get { return this.validationResult.IsValid; }
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        public IEnumerable<ModelValidationError> Errors
        {
            get { return this.validationResult.Errors; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelContext"/> class.
        /// </summary>
        public ModelContext()
            : this(new ExpandoObject())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelContext"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ModelContext(object model)
            : this(model, ModelValidationResult.Valid)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelContext"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="validationResult">The validation result.</param>
        public ModelContext(object model, ModelValidationResult validationResult)
        {
            this.Model = model;
            this.validationResult = validationResult ?? ModelValidationResult.Valid;
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="message">The message.</param>
        public void AddError(string memberName, string message)
        {
            this.validationResult = this.validationResult.AddError(memberName, message);
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="memberNames">The member names.</param>
        /// <param name="message">The message.</param>
        public void AddError(IEnumerable<string> memberNames, string message)
        {
            this.validationResult = this.validationResult.AddError(memberNames, message);
        }

        /// <summary>
        /// Clears the errors.
        /// </summary>
        public void ClearErrors()
        {
            this.validationResult = ModelValidationResult.Valid;
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void SetModel(object model)
        {
            //we don't want to overwrite the existing validation if the model is the same thing.
            if (Object.ReferenceEquals(this.Model, model))
                return;

            this.SetModel(model, ModelValidationResult.Valid);
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="result">The result.</param>
        public void SetModel(object model, ModelValidationResult result)
        {
            this.Model = GetSafeModel(model);
            this.validationResult = result ?? ModelValidationResult.Valid;
        }

        /// <summary>
        /// Provides implementation for type conversion operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations that convert an object from one type to another.
        /// </summary>
        /// <param name="binder">Provides information about the conversion operation. The binder.Type property provides the type to which the object must be converted. For example, for the statement (String)sampleObject in C# (CType(sampleObject, Type) in Visual Basic), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Type returns the <see cref="T:System.String"/> type. The binder.Explicit property provides information about the kind of conversion that occurs. It returns true for explicit conversion and false for implicit conversion.</param>
        /// <param name="result">The result of the type conversion operation.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type.IsAssignableFrom(typeof(ModelValidationResult)))
            {
                result = this.validationResult;
                return true;
            }

            var dyn = this.Model as DynamicObject;
            if (dyn != null)
                return dyn.TryConvert(binder, out result);

            if (this.Model != null && binder.Type.IsAssignableFrom(this.Model.GetType()))
            {
                result = Convert.ChangeType(this.Model, binder.Type);
                return true;
            }

            return base.TryConvert(binder, out result);
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result"/>.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var expando = this.Model as IDictionary<string, object>;
            if (expando != null)
            {
                result = expando[binder.Name];
                return true;
            }

            var dyn = this.Model as DynamicObject;
            if (dyn != null)
                return dyn.TryGetMember(binder, out result);

            var property = this.Model.GetType().GetProperty(binder.Name, BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                result = property.GetValue(this.Model, null);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Provides the implementation for operations that invoke a member. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as calling a method.
        /// </summary>
        /// <param name="binder">Provides information about the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleMethod". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="args">The arguments that are passed to the object member during the invoke operation. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, <paramref name="args[0]"/> is equal to 100.</param>
        /// <param name="result">The result of the member invocation.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var dyn = this.Model as DynamicObject;
            if (dyn != null)
                return dyn.TryInvokeMember(binder, args, out result);

            try
            {
                result = this.Model.GetType().InvokeMember(binder.Name, BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, this.Model, args);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, the <paramref name="value"/> is "Test".</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var expando = this.Model as IDictionary<string, object>;
            if (expando != null)
            {
                expando[binder.Name] = value;
                return true;
            }

            var dyn = this.Model as DynamicObject;
            if (dyn != null)
            {
                return dyn.TrySetMember(binder, value);
            }

            var property = this.Model.GetType().GetProperty(binder.Name, BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                property.SetValue(this.Model, value, null);
                return true;
            }

            return false;
        }

        private static object GetSafeModel(object model)
        {
            return (model != null && model.GetType().IsAnonymousType()) 
                ? GetExpandoObject(model) 
                : model;
        }

        private static ExpandoObject GetExpandoObject(object source)
        {
            var expandoObject = new ExpandoObject();
            IDictionary<string, object> results = expandoObject;

            foreach (var propertyInfo in source.GetType().GetProperties())
            {
                results[propertyInfo.Name] = propertyInfo.GetValue(source, null);
            }

            return expandoObject;
        }
    }
}