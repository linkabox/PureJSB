//JavaScript v1.5 
//SharpKit v4.04.5000
using System;
using System.Collections;
using System.Collections.Generic;
using SharpKit.JavaScript;
using System.ComponentModel;

//Object
[assembly: JsType(TargetType = typeof(object), OmitCasts = true)]
[assembly: JsMethod(TargetType = typeof(object), TargetMethod = "ToString", Name = "toString")]


//Numbers
[assembly: JsType(TargetType = typeof(int), OmitCasts = true)]
[assembly: JsType(TargetType = typeof(byte), OmitCasts = true)]
[assembly: JsType(TargetType = typeof(short), OmitCasts = true)]
[assembly: JsType(TargetType = typeof(double), OmitCasts = true)]
[assembly: JsType(TargetType = typeof(float), OmitCasts = true)]
[assembly: JsType(TargetType = typeof(uint), OmitCasts = true)]
[assembly: JsType(TargetType = typeof(ushort), OmitCasts = true)]
[assembly: JsType(TargetType = typeof(decimal), OmitCasts = true)]

//Type
[assembly: JsType(TargetType = typeof(Type), NativeOperatorOverloads = true)]

//Delegate
[assembly: JsType(TargetType = typeof(Delegate), NativeOperatorOverloads = true)]
[assembly: JsType(TargetType = typeof(MulticastDelegate), NativeOperatorOverloads = true)]
[assembly: JsMethod(TargetType = typeof(Delegate), TargetMethod = "Combine", Name = "$CombineDelegates", Global = true, NativeOverloads = true)]
[assembly: JsMethod(TargetType = typeof(Delegate), TargetMethod = "Remove", Name = "$RemoveDelegate", Global = true, NativeOverloads = true)]

//String
[assembly: JsType(JsMode.Clr, TargetType = typeof(string), NativeParams = true, NativeOperatorOverloads = true)]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "Format", NativeParams = false)]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "ToUpper", Name = "toUpperCase")]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "ToLower", Name = "toLowerCase")]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "IndexOf", Name = "indexOf", NativeOverloads = true)]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "LastIndexOf", Name = "lastIndexOf", NativeOverloads = true)]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "Trim", Name = "trim")]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "Substring", Name = "substr", NativeOverloads = true)]
[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "get_Chars", Name = "charAt", NativeOverloads = true)]
//[assembly: JsMethod(TargetType = typeof(string), TargetMethod = "get_Length", Name = "length", OmitParanthesis = true)]
//[assembly: JsProperty(TargetType = typeof(string), TargetProperty = "Chars", Name = "charAt", NativeField = true)]
[assembly: JsProperty(TargetType = typeof(string), TargetProperty = "Length", Name = "length", NativeField = true)]

//Array
[assembly: JsType(JsMode.Prototype, TargetType = typeof(Array), NativeArrayEnumerator = true, NativeEnumerator = false, Export = false)]
//[assembly: JsMethod(TargetType = typeof(Array), TargetMethod = "get_Length", Name = "length", OmitParanthesis = true)]
[assembly: JsProperty(TargetType = typeof(Array), TargetProperty = "Length", Name = "length", NativeField = true)]
[assembly: JsMethod(TargetType = typeof(Array), TargetMethod = "Sort", NativeOverloads = false, IgnoreGenericArguments = false)]


namespace SharpKit.JavaScript
{

    #region JsArguments
    /// <summary>
    /// An object representing the arguments to the currently executing function, and the functions that called it.
    /// </summary>
    [JsType(JsMode.Prototype, Name = "arguments", Export = false, NativeArrayEnumerator = true, NativeEnumerator = false)]
    public partial class JsArguments : IEnumerable<object>, IEnumerable
    {
        /// <summary>
        /// The zero-based index to argument values passed to the Function object.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [JsProperty(NativeIndexer = true)]
        public object this[JsNumber index] { get { return default(object); } set { } }
        /// <summary>
        /// Returns the actual number of arguments passed to a function by the caller.
        /// </summary>
        [JsProperty(NativeField = true)]
        public JsNumber length { get; set; }
        /// <summary>
        /// Returns the Function object being executed, that is, the body text of the specified Function object.
        /// </summary>
        public JsFunction callee { get; set; }

        #region IEnumerable<object> Members

        public IEnumerator<object> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    #endregion
    #region JsArray
    ///<summary>
    ///The Array object provides support for creation of arrays of any data type.
    ///</summary>
    [JsType(JsMode.Prototype, Export = false, Name = "Array", NativeEnumerator = false, NativeArrayEnumerator = true, NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsArray : IJsArrayEnumerable<object>
    {
        [JsMethod(JsonInitializers = true, OmitNewOperator = true, OmitParanthesis = true, Name = "", SharpKitVersion = "5+")]
        public JsArray() { }
        //public JsArray(JsArray array) { }
        public JsArray(JsNumber size) { }
        public JsArray(object item) { }
        public JsArray(object item1, object item2) { }
        public JsArray(object item1, object item2, params object[] items) { }
        public static implicit operator JsArray(Array array) { return default(JsArray); }
        public static implicit operator Array(JsArray array) { return default(Array); }
        public static implicit operator JsArray(object[] array) { return default(JsArray); }
        public static implicit operator object[](JsArray array) { return default(object[]); }
        ///<summary>
        ///Returns an integer value one higher than the highest element defined in an array.
        ///</summary>
        [JsProperty(NativeField = true)]
        public JsNumber length { get; set; }
        [JsProperty(NativeIndexer = true)]
        public object this[JsNumber index] { get { return default(object); } set { } }
        ///<summary>
        ///Appends new elements to an array, and returns the new length of the array.
        ///</summary>
        ///<param name="item">Optional. New elements of the Array.</param>
        public void push(object item) { }
        public void push(object item1, object item2) { }
        public void push(object item1, object item2, params object[] items) { }
        //[JsMethod(NativeOverloads = true)]
        //public object peek() { return default(object); }
        ///<summary>
        ///Removes the last element from an array and returns it.
        ///</summary>
        ///<remarks>
        ///If the array is empty, undefined is returned.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public object pop() { return default(object); }
        ///<summary>
        ///Returns a new array consisting of a combination of the current array and any additional items.
        ///</summary>
        ///<param name="items">Optional. Additional items to add to the end of the current array.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsArray concat(params JsArray[] items) { return default(JsArray); }
        ///<summary>
        ///Returns a new array consisting of a combination of the current array and any additional items.
        ///</summary>
        ///<param name="items">Optional. Additional items to add to the end of the current array.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsArray concat(params object[] items) { return default(JsArray); }
        ///<summary>
        ///Returns a JsString value consisting of all the elements of an array concatenated together and separated by the specified separator character.
        ///</summary>
        ///<param name="separator">Required. A JsString that is used to separate one element of an array from the next in the resulting String object. If omitted, the array elements are separated with a comma.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsString join(JsString separator) { return default(JsString); }
        ///<summary>
        ///Removes elements from an array and, if necessary, inserts new elements in their place, returning the deleted elements. Returns the elements removed from the array.
        ///</summary>
        ///<param name="start">Required. The zero-based location in the array from which to start removing elements.</param>
        ///<param name="deleteCount">Required. The number of elements to remove.</param>
        ///<param name="newItems">Optional. Elements to insert into the array in place of the deleted elements.</param>
        ///<remarks>
        ///The splice method modifies the array by removing the specified number of elements from position start and inserting new elements. The deleted elements are returned as a new array object.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsArray splice(JsNumber start, JsNumber deleteCount, params object[] newItems) { return default(JsArray); }
        ///<summary>
        ///Returns a section of an array.
        ///</summary>
        ///<param name="start">Required. The index to the beginning of the specified portion of the array.</param>
        ///<param name="end">Optional. The index to the end of the specified portion of the array.</param>
        ///<remarks>
        ///The slice method returns an Array object containing the specified portion of the array.
        ///The slice method copies up to, but not including, the element indicated by end. If start is negative, it is treated as length + start where length is the length of the array. If end is negative, it is treated as length + end where length is the length of the array. If end is omitted, extraction continues to the end of the array. If end occurs before start, no elements are copied to the new array.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsArray slice(JsNumber start, JsNumber end) { return default(JsArray); }
        ///<summary>
        ///Returns a section of an array.
        ///</summary>
        ///<param name="start">Required. The index to the beginning of the specified portion of the array.</param>
        ///<remarks>
        ///The slice method returns an Array object containing the specified portion of the array.
        ///The slice method copies up to, but not including, the element indicated by end. If start is negative, it is treated as length + start where length is the length of the array. If end is negative, it is treated as length + end where length is the length of the array. If end is omitted, extraction continues to the end of the array. If end occurs before start, no elements are copied to the new array.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsArray slice(JsNumber start) { return default(JsArray); }
        ///<summary>
        /// Tests whether some element in the array passes the test implemented by the provided function.
        /// Supported in Chrome, Firefox, IE 9, Opera and Safari
        /// Documentation from MDN.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public JsBoolean some(JsFunc<object, JsNumber, JsArray, JsBoolean> callback) { return default(JsBoolean); }
        ///<summary>
        /// Executes a provided function once per array element.
        /// Supported in Chrome, Firefox, IE 9, Opera and Safari
        /// Documentation from MDN.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public void forEach(JsAction<object, JsNumber, JsArray> callback) { }
        [JsMethod(NativeOverloads = true)]
        public void forEach(JsAction<object, JsNumber> callback) { }
        [JsMethod(NativeOverloads = true)]
        public void forEach(JsAction<object> callback) { }
        ///<summary>
        ///Returns an Array object with the elements reversed.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsArray reverse() { return default(JsArray); }
        ///<summary>
        ///Removes the first element from an array and returns that element.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public object shift() { return default(object); }
        ///<summary>
        ///Inserts specified elements into the beginning of an array.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public object unshift() { return default(object); }
        ///<summary>
        ///Inserts specified elements into the beginning of an array.
        ///</summary>
        ///<param name="newItems">Optional. Elements to insert at the start of the Array.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public object unshift(params object[] newItems) { return default(object); }
        ///<summary>
        ///Returns an Array object with the elements sorted. Warning: array itself is sorted internally
        ///</summary>
        ///<param name="sortFunction">Optional. OriginalValue of the function used to determine the order of the elements.</param>
        [JsMethod(NativeOverloads = true)]
        public JsArray sort(Func<object, object, JsNumber> sortFunction) { return default(JsArray); }
        ///<summary>
        ///Returns an Array object with the elements sorted. Warning: array itself is sorted internally
        ///</summary>
        ///<param name="sortFunction">Optional. OriginalValue of the function used to determine the order of the elements.</param>
        public JsArray sort(JsFunction sortFunction) { return default(JsArray); }
        ///<summary>
        ///Returns an Array object with the elements sorted.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public void sort() { }
        public IEnumerator<object> GetEnumerator() { return default(IEnumerator<object>); }
        IEnumerator IEnumerable.GetEnumerator() { return default(IEnumerator); }
        [JsMethod(Name = "push")]
        public void Add(object item) { }

        public JsArray valueOf() { return null; }
        /// <summary>
        ///The lastIndexOf() method searches the array for the specified item, and returns it's position.
        ///The search will start at the specified position, or at the end if no start position is specified, and end the search at the beginning of the array.
        ///Returns -1 if the item is not found.
        ///</summary>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber lastIndexOf(object item, JsNumber start) { return null; }
        /// <summary>
        ///The lastIndexOf() method searches the array for the specified item, and returns it's position.
        ///The search will start at the specified position, or at the end if no start position is specified, and end the search at the beginning of the array.
        ///Returns -1 if the item is not found.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber lastIndexOf(object item) { return null; }

        /// <summary>
        /// The indexOf() method searches the array for the specified item, and returns it's position.
        /// The search will start at the specified position, or at the beginning if no start position is specified, and end the search at the end of the array.
        /// If the item is present more than once, the indexOf method returns the position of the first occurence.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber indexOf(object item, JsNumber start) { return null; }
        /// <summary>
        /// The indexOf() method searches the array for the specified item, and returns it's position.
        /// The search will start at the specified position, or at the beginning if no start position is specified, and end the search at the end of the array.
        /// If the item is present more than once, the indexOf method returns the position of the first occurence.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber indexOf(object item) { return null; }

    }
    #endregion
    #region JsArray<T>
    ///<summary>
    ///The Array object provides support for creation of arrays of any data type.
    ///</summary>
    ///<typeparam name="T"></typeparam>
    [JsType(JsMode.Prototype, Name = "Array", NativeEnumerator = false, Export = false, IgnoreGenericTypeArguments = true, NativeArrayEnumerator = true, NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsArray<T> : IJsArrayEnumerable<T>
    {
        public static implicit operator JsArray(JsArray<T> x) { return null; }
        public static implicit operator T[](JsArray<T> x) { return default(T[]); }
        public static implicit operator JsArray<T>(T[] array) { return default(JsArray<T>); }
        [JsMethod(IgnoreGenericArguments = true, JsonInitializers = true, OmitNewOperator = true, OmitParanthesis = true, Name = "", SharpKitVersion = "5+")]
        public JsArray() { }
        //[JsMethod(IgnoreGenericArguments = true)]
        //public JsArray(JsArray<T> array) { }
        [JsMethod(IgnoreGenericArguments = true)]
        public JsArray(JsNumber size) { }
        [JsMethod(IgnoreGenericArguments = true)]
        public JsArray(params T[] items) { }
        [JsProperty(NativeIndexer = true)]
        public T this[JsNumber index] { get { return default(T); } set { } }
        ///<summary>
        ///Appends new elements to an array, and returns the new length of the array.
        ///</summary>
        ///<param name="items">Optional. New elements of the Array.</param>
        public void push(params T[] items) { }
        ///<summary>
        ///Appends new elements to an array, and returns the new length of the array.
        ///</summary>
        ///<param name="item">Optional. New elements of the Array.</param>
        public void push(T item) { }
        //[JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        //public T peek() { return default(T); }
        ///<summary>
        ///Removes the last element from an array and returns it.
        ///</summary>
        ///<remarks>
        ///If the array is empty, undefined is returned.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public T pop() { return default(T); }
        ///<summary>
        ///Returns a new array consisting of a combination of the current array and any additional items.
        ///</summary>
        ///<param name="items">Optional. Additional items to add to the end of the current array.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsArray<T> concat(params JsArray<T>[] items) { return default(JsArray<T>); }
        ///<summary>
        ///Returns a new array consisting of a combination of the current array and any additional items.
        ///</summary>
        ///<param name="items">Optional. Additional items to add to the end of the current array.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsArray<T> concat(params T[] items) { return default(JsArray<T>); }
        ///<summary>
        ///Removes elements from an array and, if necessary, inserts new elements in their place, returning the deleted elements. Returns the elements removed from the array.
        ///</summary>
        ///<param name="start">Required. The zero-based location in the array from which to start removing elements.</param>
        ///<param name="deleteCount">Required. The number of elements to remove.</param>
        ///<param name="newItems">Optional. Elements to insert into the array in place of the deleted elements.</param>
        ///<remarks>
        ///The splice method modifies the array by removing the specified number of elements from position start and inserting new elements. The deleted elements are returned as a new array object.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsArray<T> splice(JsNumber start, JsNumber deleteCount, params T[] newItems) { return default(JsArray<T>); }
        ///<summary>
        ///Returns a section of an array.
        ///</summary>
        ///<param name="start">Required. The index to the beginning of the specified portion of the array.</param>
        ///<param name="end">Optional. The index to the end of the specified portion of the array.</param>
        ///<remarks>
        ///The slice method returns an Array object containing the specified portion of the array.
        ///The slice method copies up to, but not including, the element indicated by end. If start is negative, it is treated as length + start where length is the length of the array. If end is negative, it is treated as length + end where length is the length of the array. If end is omitted, extraction continues to the end of the array. If end occurs before start, no elements are copied to the new array.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsArray<T> slice(JsNumber start, JsNumber end) { return default(JsArray<T>); }
        ///<summary>
        ///Returns a section of an array.
        ///</summary>
        ///<param name="start">Required. The index to the beginning of the specified portion of the array.</param>
        ///<remarks>
        ///The slice method returns an Array object containing the specified portion of the array.
        ///The slice method copies up to, but not including, the element indicated by end. If start is negative, it is treated as length + start where length is the length of the array. If end is negative, it is treated as length + end where length is the length of the array. If end is omitted, extraction continues to the end of the array. If end occurs before start, no elements are copied to the new array.
        ///</remarks>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsArray<T> slice(JsNumber start) { return default(JsArray<T>); }
        ///<summary>
        /// Tests whether some element in the array passes the test implemented by the provided function.
        /// Supported in Chrome, Firefox, IE 9, Opera and Safari
        /// Documentation from MDN.
        ///</summary>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsBoolean some(JsFunc<JsObject, JsNumber, JsArray<T>, JsBoolean> callback) { return default(JsBoolean); }
        ///<summary>
        /// Executes a provided function once per array element.
        /// Supported in Chrome, Firefox, IE 9, Opera and Safari
        /// Documentation from MDN.
        ///</summary>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public void forEach(JsAction<T, JsNumber, JsArray<T>> callback) { }
        ///<summary>
        /// Executes a provided function once per array element.
        /// Supported in Chrome, Firefox, IE 9, Opera and Safari
        /// Documentation from MDN.
        ///</summary>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public void forEach(JsAction<T, JsNumber> callback) { }
        ///<summary>
        /// Executes a provided function once per array element.
        /// Supported in Chrome, Firefox, IE 9, Opera and Safari
        /// Documentation from MDN.
        ///</summary>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public void forEach(JsAction<T> callback) { }
        ///<summary>
        ///Returns an Array object with the elements reversed.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsArray<T> reverse() { return default(JsArray<T>); }
        ///<summary>
        ///Removes the first element from an array and returns that element.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public T shift() { return default(T); }
        ///<summary>
        ///Inserts specified elements into the beginning of an array.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public T unshift() { return default(T); }
        ///<summary>
        ///Inserts specified elements into the beginning of an array.
        ///</summary>
        ///<param name="newItems">Optional. Elements to insert at the start of the Array.</param>
        ///<returns>the new length</returns>
        [JsMethod(NativeOverloads = true, IgnoreGenericArguments = true)]
        public JsNumber unshift(params T[] newItems) { return default(JsNumber); }
        ///<summary>
        ///Returns an Array object with the elements sorted. Warning: Array itself is sorted internally
        ///</summary>
        ///<param name="sortFunction">Optional. OriginalValue of the function used to determine the order of the elements.</param>
        [JsMethod(NativeOverloads = true, NativeDelegates = true)]
        public JsArray<T> sort(Func<T, T, JsNumber> sortFunction) { return null; }
        ///<summary>
        ///Returns an Array object with the elements sorted. Warning: Array itself is sorted internally
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public JsArray<T> sort() { return null; }
        public IEnumerator<T> GetEnumerator() { return default(IEnumerator<T>); }
        IEnumerator IEnumerable.GetEnumerator() { return default(IEnumerator); }
        ///<summary>
        ///Returns a JsString value consisting of all the elements of an array concatenated together and separated by the specified separator character.
        ///</summary>
        ///<param name="separator">Required. A JsString that is used to separate one element of an array from the next in the resulting String object. If omitted, the array elements are separated with a comma.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsString join(JsString separator) { return default(JsString); }
        ///<summary>
        ///Returns an integer value one higher than the highest element defined in an array.
        ///</summary>
        [JsProperty(NativeField = true)]
        public JsNumber length { get; set; }

        [JsMethod(Name = "push")]
        public void Add(T item) { }

        public JsArray valueOf() { return null; }
        /// <summary>
        ///The lastIndexOf() method searches the array for the specified item, and returns it's position.
        ///The search will start at the specified position, or at the end if no start position is specified, and end the search at the beginning of the array.
        ///Returns -1 if the item is not found.
        ///</summary>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber lastIndexOf(T item, JsNumber start) { return null; }
        /// <summary>
        ///The lastIndexOf() method searches the array for the specified item, and returns it's position.
        ///The search will start at the specified position, or at the end if no start position is specified, and end the search at the beginning of the array.
        ///Returns -1 if the item is not found.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber lastIndexOf(T item) { return null; }

        /// <summary>
        /// The indexOf() method searches the array for the specified item, and returns it's position.
        /// The search will start at the specified position, or at the beginning if no start position is specified, and end the search at the end of the array.
        /// If the item is present more than once, the indexOf method returns the position of the first occurence.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber indexOf(T item, JsNumber start) { return null; }
        /// <summary>
        /// The indexOf() method searches the array for the specified item, and returns it's position.
        /// The search will start at the specified position, or at the beginning if no start position is specified, and end the search at the end of the array.
        /// If the item is present more than once, the indexOf method returns the position of the first occurence.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> -1 if the item is not found.</returns>
        public JsNumber indexOf(T item) { return null; }

        public JsArray<R> map<R>(JsFunc<T, JsNumber, JsArray<T>, R> func, object thisArg) { return null; }
        public JsArray<R> map<R>(JsFunc<T, JsNumber, R> func, object thisArg) { return null; }
        public JsArray<R> map<R>(JsFunc<T, R> func, object thisArg) { return null; }

        public JsArray<R> map<R>(JsFunc<T, JsNumber, JsArray<T>, R> func) { return null; }
        public JsArray<R> map<R>(JsFunc<T, JsNumber, R> func) { return null; }
        public JsArray<R> map<R>(JsFunc<T, R> func) { return null; }


    }
    #endregion
    #region JsBoolean
    [JsType(JsMode.Prototype, Name = "Boolean", Export = false, NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsBoolean : JsObjectBase
    {
        public JsBoolean(object boolValue) { }
        public JsBoolean() { }
        public static implicit operator bool(JsBoolean x) { return default(bool); }
        public static implicit operator JsBoolean(bool x) { return default(JsBoolean); }
    }
    #endregion
    #region JsTypes

    [JsType(JsMode.Json)]
    [JsEnum(ValuesAsNames = true)]
    public enum JsTypes
    {
        number,
        @string,
        boolean,
        @object,
        function,
        undefined,
    }
    #endregion

    #region JsContext
    [JsType(JsMode.Global, Export = false)]
    public partial class JsContext
    {
        public static JsArguments arguments;
        [JsMethod(OmitParanthesis = true)]
        public static void debugger()
        {
        }
        ///<summary>
        ///indicates that a variable has not been assigned a value.
        ///</summary>
        [JsProperty(NativeField = true)]
        public static object undefined { get; set; }
        [JsProperty(NativeField = true)]
        public static object @null { get; set; }
        /// <summary>
        /// The typeof operator returns a string indicating the type of the unevaluated operand.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JsString @typeof(object obj) { return null; }
        /// <summary>
        /// A C# equivalant to the javascript typeof operator, with one difference, 
        /// this one returns an enum with all possible values, instead of an untyped string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [JsMethod(Name = "typeof")]
        public static JsTypes JsTypeOf(object obj) { return default(JsTypes); }
        /// <summary>
        /// Returns a reference to the ctor function of a prototype mode type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = false, Name = "", OmitParanthesis = true)]
        public static JsAction CtorOf<T>() { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsAction ActionOf(JsAction action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsAction<T> ActionOf<T>(JsAction<T> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsAction<T1, T2> ActionOf<T1, T2>(JsAction<T1, T2> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsAction<T1, T2, T3> ActionOf<T1, T2, T3>(JsAction<T1, T2, T3> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsAction<T1, T2, T3, T4> ActionOf<T1, T2, T3, T4>(JsAction<T1, T2, T3, T4> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsAction<T1, T2, T3, T4, T5> ActionOf<T1, T2, T3, T4, T5>(JsAction<T1, T2, T3, T4, T5> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsFunc<R> FuncOf<R>(JsFunc<R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsFunc<T, R> FuncOf<T, R>(JsFunc<T, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsFunc<T1, T2, R> FuncOf<T1, T2, R>(JsFunc<T1, T2, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsFunc<T1, T2, T3, R> FuncOf<T1, T2, T3, R>(JsFunc<T1, T2, T3, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsFunc<T1, T2, T3, T4, R> FuncOf<T1, T2, T3, T4, R>(JsFunc<T1, T2, T3, T4, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsFunc<T1, T2, T3, T4, T5, R> FuncOf<T1, T2, T3, T4, T5, R>(JsFunc<T1, T2, T3, T4, T5, R> func) { return null; }

        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeAction NativeActionOf(JsNativeAction action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeAction<T> NativeActionOf<T>(JsNativeAction<T> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeAction<T1, T2> NativeActionOf<T1, T2>(JsNativeAction<T1, T2> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeAction<T1, T2, T3> NativeActionOf<T1, T2, T3>(JsNativeAction<T1, T2, T3> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeAction<T1, T2, T3, T4> NativeActionOf<T1, T2, T3, T4>(JsNativeAction<T1, T2, T3, T4> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeAction<T1, T2, T3, T4, T5> NativeActionOf<T1, T2, T3, T4, T5>(JsNativeAction<T1, T2, T3, T4, T5> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeFunc<R> NativeFuncOf<R>(JsNativeFunc<R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeFunc<T, R> NativeFuncOf<T, R>(JsNativeFunc<T, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeFunc<T1, T2, R> NativeFuncOf<T1, T2, R>(JsNativeFunc<T1, T2, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeFunc<T1, T2, T3, R> NativeFuncOf<T1, T2, T3, R>(JsNativeFunc<T1, T2, T3, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeFunc<T1, T2, T3, T4, R> NativeFuncOf<T1, T2, T3, T4, R>(JsNativeFunc<T1, T2, T3, T4, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        public static JsNativeFunc<T1, T2, T3, T4, T5, R> NativeFuncOf<T1, T2, T3, T4, T5, R>(JsNativeFunc<T1, T2, T3, T4, T5, R> func) { return null; }

        [JsProperty(NativeField = true)]
        public static object @this { get; set; }
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        public static object @return(object obj) { return null; }
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        public static object @return() { return null; }
        ///<summary>
        ///Evaluates JScript code and executes it.
        ///</summary>
        ///<param name="code">A JsString value that contains valid JScript code. This JsString is parsed by the JScript parser and executed.</param>
        ///<returns></returns>
        public static object eval(JsString code) { return default(object); }
        ///<summary>
        ///Returns an integer converted from a JsString.
        ///</summary>
        ///<param name="s">A JsString to convert into a number.</param>
        ///<returns>An integer value equal to the number contained in numString. If no prefix of numString can be successfully parsed into an integer, NaN (not a number) is returned.</returns>
        public static JsNumber parseInt(JsString s) { return default(JsNumber); }
        ///<summary>
        ///Returns an integer converted from a JsString.
        ///</summary>
        ///<param name="numString">A JsString to convert into a number.</param>
        ///<param name="radix"> A value between 2 and 36 indicating the base 
        ///of the number contained in numString. If not supplied, strings with 
        ///a prefix of '0x' are considered hexadecimal and strings with a prefix 
        ///of '0' are considered octal. All other strings are considered decimal.</param>
        ///<returns>An integer value equal to the number contained in numString. 
        ///If no prefix of numString can be successfully parsed into an integer, 
        ///NaN (not a number) is returned.</returns>
        public static JsNumber parseInt(JsString numString, JsNumber radix) { return default(JsNumber); }
        ///<summary>
        ///Returns a Boolean value that indicates whether a value is the reserved value NaN (not a number).
        ///</summary>
        ///<param name="numValue">A value to be tested against NaN.</param>
        ///<returns>True if the value converted to the Number type is the NaN, otherwise false.</returns>
        public static JsBoolean isNaN(object numValue) { return default(JsBoolean); }
        ///<summary>
        ///Returns a Boolean value that indicates if a supplied number is finite.
        ///</summary>
        ///<param name="number">A numeric value.</param>
        ///<returns>True if number is any value other than NaN, negative infinity, 
        ///or positive infinity. In those three cases, it returns false.</returns>
        public static JsBoolean isFinite(double number) { return default(JsBoolean); }
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        public static void @throw(JsError error) { }
        /// <summary>
        /// Deletes a property from an object, or removes an element from an array.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The expression argument is a valid JavaScript expression that usually results in a property name or array element.
        /// If the result of expression is an object, the property specified in expression exists, and the object will not allow it to be deleted, false is returned.
        /// In all other cases, true is returned.</returns>
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        public static bool delete(object obj) { return false; }

        [JsMethod(OmitParanthesis = true, NativeOverloads = true, InsertArg1="()", OmitCommas=true)]
        public static bool @new(object obj) { return false; }
        ///<summary>
        ///Returns a floating-point number converted from a JsString.
        ///</summary>
        ///<param name="s">A JsString that contains a floating-point number.</param>
        ///<returns>A numerical value equal to the number contained in numString. If no prefix of numString can be successfully parsed into a floating-point number, NaN (not a number) is returned.</returns>
        public static float parseFloat(JsString s) { return default(float); }
        ///<summary>
        ///Returns an string converted from a value.
        ///</summary>
        ///<param name="value">A value to convert into a string.</param>
        ///<returns>An string value</returns>
        public static JsString String(object value) { return null; }
        /// <summary>
        /// Allows writing direct JavaScript code - calling this method will generate only the code inside the string parameter
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [JsMethod(OmitParanthesis = true, Name = "")]
        public static object JsCode(JsCode code) { return null; }

    }
    #endregion
    #region JsContextBase
    [JsType(JsMode.Global, Export = false)]
    public partial class JsContextBase
    {
        protected static JsArguments arguments;
        [JsMethod(OmitParanthesis = true)]
        protected static void debugger()
        {
        }
        ///<summary>
        ///indicates that a variable has not been assigned a value.
        ///</summary>
        [JsProperty(NativeField = true)]
        protected static object undefined { get; set; }
        [JsProperty(NativeField = true)]
        protected static object @null { get; set; }
        /// <summary>
        /// The typeof operator returns a string indicating the type of the unevaluated operand.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static JsString @typeof(object obj) { return null; }
        /// <summary>
        /// A C# equivalant to the javascript typeof operator, with one difference, 
        /// this one returns an enum with all possible values, instead of an untyped string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [JsMethod(Name = "typeof")]
        protected static JsTypes JsTypeOf(object obj) { return default(JsTypes); }
        /// <summary>
        /// Returns a reference to the ctor function of a prototype mode type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = false, Name = "", OmitParanthesis = true)]
        protected static JsAction CtorOf<T>() { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsAction ActionOf(JsAction action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsAction<T> ActionOf<T>(JsAction<T> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsAction<T1, T2> ActionOf<T1, T2>(JsAction<T1, T2> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsAction<T1, T2, T3> ActionOf<T1, T2, T3>(JsAction<T1, T2, T3> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsAction<T1, T2, T3, T4> ActionOf<T1, T2, T3, T4>(JsAction<T1, T2, T3, T4> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsAction<T1, T2, T3, T4, T5> ActionOf<T1, T2, T3, T4, T5>(JsAction<T1, T2, T3, T4, T5> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsFunc<R> FuncOf<R>(JsFunc<R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsFunc<T, R> FuncOf<T, R>(JsFunc<T, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsFunc<T1, T2, R> FuncOf<T1, T2, R>(JsFunc<T1, T2, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsFunc<T1, T2, T3, R> FuncOf<T1, T2, T3, R>(JsFunc<T1, T2, T3, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsFunc<T1, T2, T3, T4, R> FuncOf<T1, T2, T3, T4, R>(JsFunc<T1, T2, T3, T4, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function with instance context if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsFunc<T1, T2, T3, T4, T5, R> FuncOf<T1, T2, T3, T4, T5, R>(JsFunc<T1, T2, T3, T4, T5, R> func) { return null; }

        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeAction NativeActionOf(JsNativeAction action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeAction<T> NativeActionOf<T>(JsNativeAction<T> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeAction<T1, T2> NativeActionOf<T1, T2>(JsNativeAction<T1, T2> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeAction<T1, T2, T3> NativeActionOf<T1, T2, T3>(JsNativeAction<T1, T2, T3> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeAction<T1, T2, T3, T4> NativeActionOf<T1, T2, T3, T4>(JsNativeAction<T1, T2, T3, T4> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeAction<T1, T2, T3, T4, T5> NativeActionOf<T1, T2, T3, T4, T5>(JsNativeAction<T1, T2, T3, T4, T5> action) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeFunc<R> NativeFuncOf<R>(JsNativeFunc<R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeFunc<T, R> NativeFuncOf<T, R>(JsNativeFunc<T, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeFunc<T1, T2, R> NativeFuncOf<T1, T2, R>(JsNativeFunc<T1, T2, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeFunc<T1, T2, T3, R> NativeFuncOf<T1, T2, T3, R>(JsNativeFunc<T1, T2, T3, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeFunc<T1, T2, T3, T4, R> NativeFuncOf<T1, T2, T3, T4, R>(JsNativeFunc<T1, T2, T3, T4, R> func) { return null; }
        /// <summary>
        /// Returns a reference to a javascript function without instance context even if applicable
        /// </summary>
        [JsMethod(IgnoreGenericArguments = true, Name = "", OmitParanthesis = true)]
        protected static JsNativeFunc<T1, T2, T3, T4, T5, R> NativeFuncOf<T1, T2, T3, T4, T5, R>(JsNativeFunc<T1, T2, T3, T4, T5, R> func) { return null; }

        [JsProperty(NativeField = true)]
        protected static object @this { get; set; }
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        protected static object @return(object obj) { return null; }
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        protected static object @return() { return null; }
        ///<summary>
        ///Evaluates JScript code and executes it.
        ///</summary>
        ///<param name="code">A JsString value that contains valid JScript code. This JsString is parsed by the JScript parser and executed.</param>
        ///<returns></returns>
        protected static object eval(JsString code) { return default(object); }
        ///<summary>
        ///Returns an integer converted from a JsString.
        ///</summary>
        ///<param name="s">A JsString to convert into a number.</param>
        ///<returns>An integer value equal to the number contained in numString. If no prefix of numString can be successfully parsed into an integer, NaN (not a number) is returned.</returns>
        protected static JsNumber parseInt(JsString s) { return default(JsNumber); }
        ///<summary>
        ///Returns an integer converted from a JsString.
        ///</summary>
        ///<param name="numString">A JsString to convert into a number.</param>
        ///<param name="radix"> A value between 2 and 36 indicating the base 
        ///of the number contained in numString. If not supplied, strings with 
        ///a prefix of '0x' are considered hexadecimal and strings with a prefix 
        ///of '0' are considered octal. All other strings are considered decimal.</param>
        ///<returns>An integer value equal to the number contained in numString. 
        ///If no prefix of numString can be successfully parsed into an integer, 
        ///NaN (not a number) is returned.</returns>
        protected static JsNumber parseInt(JsString numString, JsNumber radix) { return default(JsNumber); }
        ///<summary>
        ///Returns a Boolean value that indicates whether a value is the reserved value NaN (not a number).
        ///</summary>
        ///<param name="numValue">A value to be tested against NaN.</param>
        ///<returns>True if the value converted to the Number type is the NaN, otherwise false.</returns>
        protected static JsBoolean isNaN(object numValue) { return default(JsBoolean); }
        ///<summary>
        ///Returns a Boolean value that indicates if a supplied number is finite.
        ///</summary>
        ///<param name="number">A numeric value.</param>
        ///<returns>True if number is any value other than NaN, negative infinity, 
        ///or positive infinity. In those three cases, it returns false.</returns>
        protected static JsBoolean isFinite(double number) { return default(JsBoolean); }
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        protected static void @throw(JsError error) { }
        /// <summary>
        /// Deletes a property from an object, or removes an element from an array.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The expression argument is a valid JavaScript expression that usually results in a property name or array element.
        /// If the result of expression is an object, the property specified in expression exists, and the object will not allow it to be deleted, false is returned.
        /// In all other cases, true is returned.</returns>
        [JsMethod(OmitParanthesis = true, NativeOverloads = true)]
        protected static bool delete(object obj) { return false; }
        ///<summary>
        ///Returns a floating-point number converted from a JsString.
        ///</summary>
        ///<param name="s">A JsString that contains a floating-point number.</param>
        ///<returns>A numerical value equal to the number contained in numString. If no prefix of numString can be successfully parsed into a floating-point number, NaN (not a number) is returned.</returns>
        protected static float parseFloat(JsString s) { return default(float); }
        ///<summary>
        ///Returns an string converted from a value.
        ///</summary>
        ///<param name="value">A value to convert into a string.</param>
        ///<returns>An string value</returns>
        protected static JsString String(object value) { return null; }
        /// <summary>
        /// Allows writing direct JavaScript code - calling this method will generate only the code inside the string parameter
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [JsMethod(OmitParanthesis = true, Name = "")]
        protected static object JsCode(JsCode code) { return null; }
    }
    #endregion

    #region JsDate
    [JsType(JsMode.Prototype, Export = false, Name = "Date", NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsDate : JsObjectBase
    {
        public static JsNumber operator -(JsDate date1, JsDate date2) { return default(JsNumber); }
        public static bool operator <(JsDate date1, JsDate date2) { return default(bool); }
        public static bool operator <=(JsDate date1, JsDate date2) { return default(bool); }
        public static bool operator >(JsDate date1, JsDate date2) { return default(bool); }
        public static bool operator >=(JsDate date1, JsDate date2) { return default(bool); }
        public JsDate() { }
        public JsDate(long value) { }
        public JsDate(JsString value) { }
        public JsDate(int year, int month, int date) { }
        public JsDate(int year, int month, int date, int hours) { }
        public JsDate(int year, int month, int date, int hours, int minutes) { }
        public JsDate(int year, int month, int date, int hours, int minutes, int seconds) { }
        public JsDate(int year, int month, int date, int hours, int minutes, int seconds, int ms) { }

        ///<summary>
        ///Returns the year value in the Date object using local time.
        ///</summary>
        ///<returns>the year as an absolute number. For example, the year 1976 is returned as 1976.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getFullYear() { return default(JsNumber); }
        ///<summary>
        ///Returns the month value in the Date object using local time.
        ///</summary>
        ///<returns>integer between 0 and 11 indicating the month value in the Date object. The integer returned is not the traditional number used to indicate the month. It is one less. If "Jan 5, 1996 08:47:00" is stored in a Date object, getMonth returns 0.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getMonth() { return default(JsNumber); }
        ///<summary>
        ///Returns the day-of-the-month value in a Date object using local time.
        ///</summary>
        ///<returns> an integer between 1 and 31 that represents the day-of-the-month value in the Date object.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getDate() { return default(JsNumber); }
        ///<summary>
        ///Returns the hours value in a Date object using local time.
        ///</summary>
        ///<returns>an integer between 0 and 23, indicating the number of hours since midnight. A zero occurs in two situations: the time is before 1:00:00 am, or the time was not stored in the Date object when the object was created. The only way to determine which situation you have is to also check the minutes and seconds for zero values. If they are all zeroes, it is nearly certain that the time was not stored in the Date object.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getHours() { return default(JsNumber); }
        ///<summary>
        ///Returns the minutes value in a Date object using local time.
        ///</summary>
        ///<returns>integer between 0 and 59 equal to the minutes value stored in the Date object. A zero is returned in two situations: when the time is less than one minute after the hour, or when the time was not stored in the Date object when the object was created. The only way to determine which situation you have is to also check the hours and seconds for zero values. If they are all zeroes, it is nearly certain that the time was not stored in the Date object.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getMinutes() { return default(JsNumber); }
        ///<summary>
        ///Returns the seconds value in a Date object using local time.
        ///</summary>
        ///<returns> integer between 0 and 59 indicating the seconds value of the indicated Date object. A zero is returned in two situations. One occurs when the time is less than one second into the current minute. The other occurs when the time was not stored in the Date object when the object was created. The only way to determine which situation you have is to also check the hours and minutes for zero values. If they are all zeroes, it is nearly certain that the time was not stored in the Date object.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getSeconds() { return default(JsNumber); }
        ///<summary>
        ///Returns the milliseconds value in a Date object using local time.
        ///</summary>
        ///<returns>The millisecond value returned can range from 0-999.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getMilliseconds() { return default(JsNumber); }
        ///<summary>
        ///Sets the month value in the Date object using local time.
        ///</summary>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively.</param>
        [JsMethod(NativeOverloads = true)]
        public void setMonth(JsNumber numMonth) { }
        ///<summary>
        ///Sets the month value in the Date object using local time.
        ///</summary>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively.</param>
        ///<param name="dateVal">A numeric value representing the day of the month. If this value is not supplied, the value from a call to the getDate method is used.</param>
        [JsMethod(NativeOverloads = true)]
        public void setMonth(JsNumber numMonth, JsNumber dateVal) { }
        ///<summary>
        ///Sets the hour value in the Date object using local time.
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setHours(JsNumber numHours) { }
        ///<summary>
        ///Sets the hour value in the Date object using local time.
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        ///<param name="numMin">A numeric value equal to the minutes value. </param>
        [JsMethod(NativeOverloads = true)]
        public void setHours(JsNumber numHours, JsNumber numMin) { }
        ///<summary>
        ///Sets the hour value in the Date object using local time.
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        ///<param name="numMin">A numeric value equal to the minutes value. </param>
        ///<param name="numSec">A numeric value equal to the seconds value. </param>
        [JsMethod(NativeOverloads = true)]
        public void setHours(JsNumber numHours, JsNumber numMin, JsNumber numSec) { }
        ///<summary>
        ///Sets the hour value in the Date object using local time.
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        ///<param name="numMin">A numeric value equal to the minutes value. </param>
        ///<param name="numSec">A numeric value equal to the seconds value. </param>
        ///<param name="numMilli">A numeric value equal to the milliseconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setHours(JsNumber numHours, JsNumber numMin, JsNumber numSec, JsNumber numMilli) { }
        ///<summary>
        ///Sets the minutes value in the Date object using local time.
        ///</summary>
        ///<param name="numMinutes">A numeric value equal to the minutes value. Must be supplied if either of the following arguments is used.</param>
        [JsMethod(NativeOverloads = true)]
        public void setMinutes(JsNumber numMinutes) { }
        ///<summary>
        ///Sets the minutes value in the Date object using local time.
        ///</summary>
        ///<param name="numMinutes">A numeric value equal to the minutes value. Must be supplied if either of the following arguments is used.</param>
        ///<param name="numSeconds">A numeric value equal to the seconds value. Must be supplied if the numMilli argument is used.</param>
        [JsMethod(NativeOverloads = true)]
        public void setMinutes(JsNumber numMinutes, JsNumber numSeconds) { }
        ///<summary>
        ///Sets the minutes value in the Date object using local time.
        ///</summary>
        ///<param name="numMinutes">A numeric value equal to the minutes value. Must be supplied if either of the following arguments is used.</param>
        ///<param name="numSeconds">A numeric value equal to the seconds value. Must be supplied if the numMilli argument is used.</param>
        ///<param name="numMilli">A numeric value equal to the milliseconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setMinutes(JsNumber numMinutes, JsNumber numSeconds, JsNumber numMilli) { }
        ///<summary>
        ///Sets the seconds value in the Date object using local time.
        ///</summary>
        ///<param name="numSeconds">A numeric value equal to the seconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setSeconds(JsNumber numSeconds) { }
        ///<summary>
        ///Sets the seconds value in the Date object using local time.
        ///</summary>
        ///<param name="numSeconds">A numeric value equal to the seconds value.</param>
        ///<param name="numMilli">A numeric value equal to the milliseconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setSeconds(JsNumber numSeconds, JsNumber numMilli) { }
        ///<summary>
        ///Sets the milliseconds value in the Date object using local time.
        ///</summary>
        ///<param name="value">A numeric value equal to the millisecond value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setMilliseconds(JsNumber value) { }
        ///<summary>
        ///Sets the date and time value in the Date object.
        ///</summary>
        ///<param name="milliseconds">A numeric value representing the number of elapsed milliseconds since midnight, January 1, 1970 GMT.</param>
        [JsMethod(NativeOverloads = true)]
        public void setTime(JsNumber milliseconds) { }
        ///<summary>
        ///Sets the numeric day of the month in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numDate">A numeric value equal to the day of the month.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCDate(JsNumber numDate) { }
        ///<summary>
        ///Sets the year value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numYear">A numeric value equal to the year.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCFullYear(JsNumber numYear) { }
        ///<summary>
        ///Sets the year value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numYear">A numeric value equal to the year.</param>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively. Must be supplied if numDate is supplied.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCFullYear(JsNumber numYear, JsNumber numMonth) { }
        ///<summary>
        ///Sets the year value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numYear">A numeric value equal to the year.</param>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively. Must be supplied if numDate is supplied.</param>
        ///<param name="numDate">A numeric value equal to the day of the month.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCFullYear(JsNumber numYear, JsNumber numMonth, JsNumber numDate) { }
        ///<summary>
        ///Sets the hours value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCHours(JsNumber numHours) { }
        ///<summary>
        ///Sets the hours value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        ///<param name="numMin">A numeric value equal to the minutes value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCHours(JsNumber numHours, JsNumber numMin) { }
        ///<summary>
        ///Sets the hours value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        ///<param name="numMin">A numeric value equal to the minutes value.</param>
        ///<param name="numSec">A numeric value equal to the seconds value</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCHours(JsNumber numHours, JsNumber numMin, JsNumber numSec) { }
        ///<summary>
        ///Sets the hours value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numHours">A numeric value equal to the hours value.</param>
        ///<param name="numMin">A numeric value equal to the minutes value.</param>
        ///<param name="numSec">A numeric value equal to the seconds value</param>
        ///<param name="numMilli">A numeric value equal to the milliseconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCHours(JsNumber numHours, JsNumber numMin, JsNumber numSec, JsNumber numMilli) { }
        ///<summary>
        ///Sets the milliseconds value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numMilli">A numeric value equal to the millisecond value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCMilliseconds(JsNumber numMilli) { }
        ///<summary>
        ///Sets the minutes value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numMinutes">A numeric value equal to the minutes value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCMinutes(JsNumber numMinutes) { }
        ///<summary>
        ///Sets the minutes value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numMinutes">A numeric value equal to the minutes value.</param>
        ///<param name="numSeconds">A numeric value equal to the seconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCMinutes(JsNumber numMinutes, JsNumber numSeconds) { }
        ///<summary>
        ///Sets the minutes value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numMinutes">A numeric value equal to the minutes value.</param>
        ///<param name="numSeconds">A numeric value equal to the seconds value.</param>
        ///<param name="numMilli">A numeric value equal to the milliseconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCMinutes(JsNumber numMinutes, JsNumber numSeconds, JsNumber numMilli) { }
        ///<summary>
        ///Sets the month value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCMonth(JsNumber numMonth) { }
        ///<summary>
        ///Sets the month value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively.</param>
        ///<param name="dateVal">A numeric value representing the day of the month. If it is not supplied, the value from a call to the getUTCDate method is used.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCMonth(JsNumber numMonth, JsNumber dateVal) { }
        ///<summary>
        ///Sets the seconds value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numSeconds">A numeric value equal to the seconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCSeconds(JsNumber numSeconds) { }
        ///<summary>
        ///Sets the seconds value in the Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<param name="numSeconds">A numeric value equal to the seconds value.</param>
        ///<param name="numMilli">A numeric value equal to the milliseconds value.</param>
        [JsMethod(NativeOverloads = true)]
        public void setUTCSeconds(JsNumber numSeconds, JsNumber numMilli) { }
        ///<summary>
        ///Sets the year value in the Date object.
        ///</summary>
        ///<param name="numYear">This method is obsolete, and is maintained for backwards compatibility only. Use the setFullYear method instead. For the years 1900 through 1999, this is a numeric value equal to the year minus 1900. For dates outside that range, this is a 4-digit numeric value.</param>
        [JsMethod(NativeOverloads = true)]
        [Obsolete]
        public void setYear(JsNumber numYear) { }
        ///<summary>
        ///Returns a date as a JsString value.
        ///</summary>
        ///<returns>A JsString value containing the date, in the current time zone, in a convenient, easily read format.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toDateString() { return default(JsString); }
        ///<summary>
        ///Returns a date converted to a JsString using Greenwich Mean Time(GMT).
        ///Obsolete, and is provided for backwards compatibility only. It is recommended that you use the toUTCString method instead.
        ///</summary>
        ///<returns>A String object that contains the date formatted using GMT convention. The format of the return value is as follows: "05 Jan 1996 00:00:00 GMT."</returns>
        [Obsolete]
        [JsMethod(NativeOverloads = true)]
        public JsString toGMTString() { return default(JsString); }
        ///<summary>
        ///Used by the JSON.stringify method to enable the transformation of an object's data of before the JSON serialization.
        ///</summary>
        ///<returns>Returns an ISO-formatted date JsString for the UTC time zone (denoted by the suffix Z).</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toJSON() { return default(JsString); }
        ///<summary>
        ///Returns a date as a JsString value appropriate to the host environment's current locale.
        ///</summary>
        ///<returns>A JsString value that contains a date, in the current time zone, in an easily read format. The date is in the default format of the host environment's current locale. The return value of this method cannot be relied upon in scripting, as it will vary from computer to computer. The toLocaleDateString method should only be used to format display – never as part of a computation.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toLocaleDateString() { return default(JsString); }
        ///<summary>
        ///Returns a time as a JsString value appropriate to the host environment's current locale.
        ///</summary>
        ///<returns>A JsString value that contains a time, in the current time zone, in an easily read format. The time is in the default format of the host environment's current locale. The return value of this method cannot be relied upon in scripting, as it will vary from computer to computer. The toLocaleTimeString method should only be used to format display – never as part of a computation.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toLocaleTimeString() { return default(JsString); }
        ///<summary>
        ///Returns a time as a JsString value.
        ///</summary>
        ///<returns>A JsString value containing the time, in the current time zone, in a convenient, easily read format.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toTimeString() { return default(JsString); }
        ///<summary>
        ///Returns a date converted to a JsString using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>A String object that contains the date formatted using UTC convention in a convenient, easily read form.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toUTCString() { return default(JsString); }
        ///<summary>
        ///Returns the number of milliseconds between midnight, January 1, 1970 Universal Coordinated Time (UTC) (or GMT) and the supplied date.
        ///</summary>
        ///<param name="year">The full year designation is required for cross-century date accuracy. If year is between 0 and 99 is used, then year is assumed to be 1900 + year.</param>
        ///<param name="month">The month as an integer between 0 and 11 (January to December).</param>
        ///<param name="day"> The date as an integer between 1 and 31.</param>
        ///<returns>The number of milliseconds between midnight, January 1, 1970 UTC and the supplied date. This return value can be used in the setTime method and in the Date object constructor. If the value of an argument is greater than its range, or is a negative number, other stored values are modified accordingly. For example, if you specify 150 seconds, JScript redefines that number as two minutes and 30 seconds.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsString UTC(JsNumber year, JsNumber month, JsNumber day) { return default(JsString); }
        ///<summary>
        ///Returns the number of milliseconds between midnight, January 1, 1970 Universal Coordinated Time (UTC) (or GMT) and the supplied date.
        ///</summary>
        ///<param name="year">The full year designation is required for cross-century date accuracy. If year is between 0 and 99 is used, then year is assumed to be 1900 + year.</param>
        ///<param name="month">The month as an integer between 0 and 11 (January to December).</param>
        ///<param name="day"> The date as an integer between 1 and 31.</param>
        ///<param name="hours">An integer from 0 to 23 (midnight to 11pm) that specifies the hour.</param>
        ///<returns>The number of milliseconds between midnight, January 1, 1970 UTC and the supplied date. This return value can be used in the setTime method and in the Date object constructor. If the value of an argument is greater than its range, or is a negative number, other stored values are modified accordingly. For example, if you specify 150 seconds, JScript redefines that number as two minutes and 30 seconds.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsString UTC(JsNumber year, JsNumber month, JsNumber day, JsNumber hours) { return default(JsString); }
        ///<summary>
        ///Returns the number of milliseconds between midnight, January 1, 1970 Universal Coordinated Time (UTC) (or GMT) and the supplied date.
        ///</summary>
        ///<param name="year">The full year designation is required for cross-century date accuracy. If year is between 0 and 99 is used, then year is assumed to be 1900 + year.</param>
        ///<param name="month">The month as an integer between 0 and 11 (January to December).</param>
        ///<param name="day"> The date as an integer between 1 and 31.</param>
        ///<param name="hours">An integer from 0 to 23 (midnight to 11pm) that specifies the hour.</param>
        ///<param name="minutes"> An integer from 0 to 59 that specifies the minutes.</param>
        ///<returns>The number of milliseconds between midnight, January 1, 1970 UTC and the supplied date. This return value can be used in the setTime method and in the Date object constructor. If the value of an argument is greater than its range, or is a negative number, other stored values are modified accordingly. For example, if you specify 150 seconds, JScript redefines that number as two minutes and 30 seconds.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsString UTC(JsNumber year, JsNumber month, JsNumber day, JsNumber hours, JsNumber minutes) { return default(JsString); }
        ///<summary>
        ///Returns the number of milliseconds between midnight, January 1, 1970 Universal Coordinated Time (UTC) (or GMT) and the supplied date.
        ///</summary>
        ///<param name="year">The full year designation is required for cross-century date accuracy. If year is between 0 and 99 is used, then year is assumed to be 1900 + year.</param>
        ///<param name="month">The month as an integer between 0 and 11 (January to December).</param>
        ///<param name="day"> The date as an integer between 1 and 31.</param>
        ///<param name="hours">An integer from 0 to 23 (midnight to 11pm) that specifies the hour.</param>
        ///<param name="minutes"> An integer from 0 to 59 that specifies the minutes.</param>
        ///<param name="seconds">An integer from 0 to 59 that specifies the seconds.</param>
        ///<returns>The number of milliseconds between midnight, January 1, 1970 UTC and the supplied date. This return value can be used in the setTime method and in the Date object constructor. If the value of an argument is greater than its range, or is a negative number, other stored values are modified accordingly. For example, if you specify 150 seconds, JScript redefines that number as two minutes and 30 seconds.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsString UTC(JsNumber year, JsNumber month, JsNumber day, JsNumber hours, JsNumber minutes, JsNumber seconds) { return default(JsString); }
        ///<summary>
        ///Returns the number of milliseconds between midnight, January 1, 1970 Universal Coordinated Time (UTC) (or GMT) and the supplied date.
        ///</summary>
        ///<param name="year">The full year designation is required for cross-century date accuracy. If year is between 0 and 99 is used, then year is assumed to be 1900 + year.</param>
        ///<param name="month">The month as an integer between 0 and 11 (January to December).</param>
        ///<param name="day"> The date as an integer between 1 and 31.</param>
        ///<param name="hours">An integer from 0 to 23 (midnight to 11pm) that specifies the hour.</param>
        ///<param name="minutes"> An integer from 0 to 59 that specifies the minutes.</param>
        ///<param name="seconds">An integer from 0 to 59 that specifies the seconds.</param>
        ///<param name="ms">An integer from 0 to 999 that specifies the milliseconds.</param>
        ///<returns>The number of milliseconds between midnight, January 1, 1970 UTC and the supplied date. This return value can be used in the setTime method and in the Date object constructor. If the value of an argument is greater than its range, or is a negative number, other stored values are modified accordingly. For example, if you specify 150 seconds, JScript redefines that number as two minutes and 30 seconds.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber UTC(JsNumber year, JsNumber month, JsNumber day, JsNumber hours, JsNumber minutes, JsNumber seconds, JsNumber ms) { return null; }
        ///<summary>
        ///Returns the primitive value of the specified object.
        ///</summary>
        ///<returns>The stored time value in milliseconds since midnight, January 1, 1970 UTC.</returns>
        [JsMethod(NativeOverloads = true)]
        public new JsNumber valueOf() { return default(JsNumber); }
        ///<summary>
        ///Returns the time value in a Date object. 
        ///</summary>
        ///<returns>
        ///An integer value representing the number of milliseconds between midnight, January 1, 1970 and the time value in the Date object. 
        ///The range of dates is approximately 285,616 years from either side of midnight, January 1, 1970. 
        ///Negative numbers indicate dates prior to 1970. </returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getTime() { return default(JsNumber); }
        ///<summary>
        ///Returns the day of the week value in a Date object using local time.
        ///</summary>
        ///<returns>an integer between 0 and 6 representing the day of the week and corresponds to a day of the week as follows: 
        ///0 Sunday 1 Monday 2 Tuesday 3 Wednesday 4 Thursday 5 Friday 6 Saturday</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getDay() { return default(JsNumber); }
        ///<summary>
        ///Returns the difference in minutes between the time on the host computer and Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>An integer value representing the number of minutes between the time on the current machine and UTC. These values are appropriate to the computer the script is executed on. If it is called from a server script, the return value is appropriate to the server. If it is called from a client script, the return value is appropriate to the client.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getTimezoneOffset() { return default(JsNumber); }
        ///<summary>
        ///Returns the day-of-the-month value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>an integer between 1 and 31 that represents the day-of-the-month value in the Date object.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCDate() { return default(JsNumber); }
        ///<summary>
        ///Returns the day of the week value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>an integer between 0 and 6 representing the day of the week and corresponds to a day of the week as follows: 
        ///0 Sunday 1 Monday 2 Tuesday 3 Wednesday 4 Thursday 5 Friday 6 Saturday</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCDay() { return default(JsNumber); }
        ///<summary>
        ///Returns the year value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>The year as an absolute number. This avoids the year 2000 problem where dates beginning with January 1, 2000 are confused with those beginning with January 1, 1900.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCFullYear() { return default(JsNumber); }
        ///<summary>
        ///Returns the hours value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>An integer between 0 and 23 indicating the number of hours since midnight. A zero occurs in two situations: the time is before 1:00:00 A.M., or a time was not stored in the Date object when the object was created. The only way to determine which situation you have is to also check the minutes and seconds for zero values. If they are all zeroes, it is nearly certain that the time was not stored in the Date object.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCHours() { return default(JsNumber); }
        ///<summary>
        ///Returns the milliseconds value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>The millisecond value returned can range from 0-999.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCMilliseconds() { return default(JsNumber); }
        ///<summary>
        ///Returns the minutes value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>An integer between 0 and 59 equal to the number of minutes value in the Date object. A zero occurs in two situations: the time is less than one minute after the hour, or a time was not stored in the Date object when the object was created. The only way to determine which situation you have is to also check the hours and seconds for zero values. If they are all zeroes, it is nearly certain that the time was not stored in the Date object.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCMinutes() { return default(JsNumber); }
        ///<summary>
        ///Returns the month value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>An integer between 0 and 11 indicating the month value in the Date object. The integer returned is not the traditional number used to indicate the month. It is one less. If "Jan 5, 1996 08:47:00.0" is stored in a Date object, getUTCMonth returns 0.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCMonth() { return default(JsNumber); }
        ///<summary>
        ///Returns the seconds value in a Date object using Universal Coordinated Time (UTC).
        ///</summary>
        ///<returns>An integer between 0 and 59 indicating the seconds value of the indicated Date object. A zero occurs in two situations: the time is less than one second into the current minute, or a time was not stored in the Date object when the object was created. The only way to determine which situation you have is to also check the minutes and hours for zero values. If they are all zeroes, it is nearly certain that the time was not stored in the Date object..</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber getUTCSeconds() { return default(JsNumber); }
        ///<summary>
        ///Returns the year value in a Date object. This method is obsolete, and is provided for backwards compatibility only. Use the getFullYear method instead. For the years 1900 though 1999, the year is a 2-digit integer value returned as the difference between the stored year and 1900. For dates outside that period, the 4-digit year is returned. For example, 1996 is returned as 96, but 1825 and 2025 are returned as-is.
        ///</summary>
        [Obsolete]
        [JsMethod(NativeOverloads = true)]
        public JsNumber getYear() { return default(JsNumber); }
        ///<summary>
        ///Parses a JsString containing a date, and returns the number of milliseconds between that date and midnight, January 1, 1970.
        ///</summary>
        ///<param name="dateVal">Either a JsString containing a date in a format such as "Jan 5, 1996 08:47:00" or a VT_DATE value retrieved from an ActiveX® object or other object.</param>
        ///<returns>An integer value representing the number of milliseconds between midnight, January 1, 1970 and the date supplied in dateVal.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber parse(JsString dateVal) { return default(JsNumber); }
        ///<summary>
        ///Sets the numeric day-of-the-month value of the Date object using local time.
        ///</summary>
        ///<param name="numDate">A numeric value equal to the day of the month.</param>
        [JsMethod(NativeOverloads = true)]
        public void setDate(JsNumber numDate) { }
        ///<summary>
        ///Sets the year value in the Date object using local time.
        ///</summary>
        ///<param name="numYear">A numeric value equal to the year.</param>
        [JsMethod(NativeOverloads = true)]
        public void setFullYear(JsNumber numYear) { }
        ///<summary>
        ///Sets the year value in the Date object using local time.
        ///</summary>
        ///<param name="numYear">A numeric value equal to the year.</param>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively. Must be supplied if numDate is supplied.</param>
        [JsMethod(NativeOverloads = true)]
        public void setFullYear(JsNumber numYear, JsNumber numMonth) { }
        ///<summary>
        ///Sets the year value in the Date object using local time.
        ///</summary>
        ///<param name="numYear">A numeric value equal to the year.</param>
        ///<param name="numMonth">A numeric value equal to the month. The value for January is 0, and other month values follow consecutively. Must be supplied if numDate is supplied.</param>
        ///<param name="numdate">A numeric value equal to the day of the month.</param>
        [JsMethod(NativeOverloads = true)]
        public void setFullYear(JsNumber numYear, JsNumber numMonth, JsNumber numdate) { }
        /// <summary>
        /// Return the date in ISO 8601 format.
        /// </summary>
        [JsMethod(NativeOverloads = true)]
        public JsString toISOString() { return default(JsString); }
    }

    #endregion
    #region JsError
    [JsType(JsMode.Prototype, Export = false, Name = "Error", PropertiesAsFields = true, NativeCasts = true)]
    public partial class JsError : Exception
    {
        public JsError() { }
        public JsError(JsString msg) { }
        //public JsError(JsString msg, JsString filename) { }
        //public JsError(JsString msg, JsString filename, JsNumber lineNumber) { }
        ///<summary>
        ///Returns or sets the descriptive JsString associated with a specific error.
        ///</summary>
        [JsProperty(NativeField = true)]
        public JsString description { get; set; }
        ///<summary>
        ///Returns an error message JsString.
        ///</summary>
        [JsProperty(NativeField = true)]
        public JsString message { get; set; }
        /// <summary>
        /// Returns the name of an error.
        /// When a runtime error occurs, the name property is set to one of the following native exception types:
        /// ConversionError		This error occurs whenever there is an attempt to convert an object into something to which it cannot be converted.
        /// RangeError				This error occurs when a function is supplied with an argument that has exceeded its allowable range. For example, this error occurs if you attempt to construct an Array object with a length that is not a valid positive integer.
        /// ReferenceError		This error occurs when an invalid reference has been detected. This error will occur, for example, if an expected reference is null.
        /// RegExpError				This error occurs when a compilation error occurs with a regular expression. Once the regular expression is compiled, however, this error cannot occur. This example will occur, for example, when a regular expression is declared with a pattern that has an invalid syntax, or flags other than i, g, or m, or if it contains the same flag more than once.
        ///	SyntaxError				This error occurs when source text is parsed and that source text does not follow correct syntax. This error will occur, for example, if the eval function is called with an argument that is not valid program text.
        ///	TypeError					This error occurs whenever the actual type of an operand does not match the expected type. An example of when this error occurs is a function call made on something that is not an object or does not support the call.
        ///	URIError					This error occurs when an illegal Uniform Resource Indicator (URI) is detected. For example, this is error occurs when an illegal character is found in a JsString being encoded or decoded.
        /// </summary>
        [JsProperty(NativeField = true)]
        public JsString name { get; set; }
        ///<summary>
        ///Returns or sets the numeric value associated with a specific error. The Error object's default property is number.
        ///</summary>
        [JsProperty(NativeField = true)]
        public JsString number { get; set; }
        #region Exception Hiding
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override IDictionary Data { get { return base.Data; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Exception GetBaseException() { return base.GetBaseException(); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { base.GetObjectData(info, context); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string HelpLink { get { return base.HelpLink; } set { base.HelpLink = value; } }
        [JsProperty(NativeField = true, Name = "message")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Message { get { return base.Message; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Source { get { return base.Source; } set { base.Source = value; } }
        [JsProperty(NativeField = true, Name = "stack")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string StackTrace { get { return base.StackTrace; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Exception InnerException { get { return base.InnerException; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Reflection.MethodBase TargetSite { get { return base.TargetSite; } }
        #endregion
        #region Object Hiding
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return base.ToString(); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool Equals(object objA, object objB) { return object.Equals(objA, objB); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool ReferenceEquals(object objA, object objB) { return object.ReferenceEquals(objA, objB); }
        #endregion
        public JsString toString() { return default(JsString); }
    }

    [JsType(JsMode.Prototype, Export = false, Name = "Error", PropertiesAsFields = true, NativeError = true)]
    public partial class JsNativeError : JsError
    {
        public JsNativeError() : base() { }
        public JsNativeError(JsString msg) : base(msg) { }
    }

    #endregion
    #region JsFunction
    [JsType(JsMode.Prototype, Export = false, Name = "Function", NativeCasts = true)]
    public partial class JsFunction : JsObjectBase
    {
        public JsFunction(params JsString[] prmsAndBody) { }
        ///<summary>
        ///Calls a method of an object, substituting another object for the current object.
        ///</summary>
        ///<param name="thisArg">The object to be used as the current object.</param>
        ///<param name="args">List of arguments to be passed to the method.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public object call(object thisArg, params object[] args) { return default(object); }
        ///<summary>
        ///Calls a method of an object, substituting another object for the current object.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public object call() { return default(object); }
        ///<summary>
        ///Calls a method of an object, substituting another object for the current object.
        ///</summary>
        ///<param name="thisArg">The object to be used as the current object.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public object call(object thisArg) { return default(object); }
        ///<summary>
        ///Applies a method of an object, substituting another object for the current object.
        ///</summary>
        ///<param name="thisArg">The object to be used as the current object.</param>
        ///<param name="args">Array of arguments to be passed to the function.</param>
        ///<returns>The return value of the function</returns>
        [JsMethod(NativeOverloads = true)]
        public object apply(object thisArg, object[] args) { return default(object); }
        ///<summary>
        ///Applies a method of an object, substituting another object for the current object.
        ///</summary>
        ///<param name="obj">The object to be used as the current object.</param>
        ///<returns>The return value of the function</returns>
        [JsMethod(NativeOverloads = true)]
        public object apply(object obj) { return default(object); }

        /// <summary>
        /// Returns a reference to the function that invoked the current function.
        /// </summary>
        public JsFunction caller { get; set; }

        ///<summary>
        ///Returns a reference to the prototype for a class of objects.
        ///</summary>
        public JsObject prototype;


    }
    #endregion
    #region JsMath
    [JsType(Export = false, Name = "Math")]
    public static partial class JsMath
    {
        ///<summary>
        ///Returns the mathematical constant e, the base of natural logarithms. The E property is approximately equal to 2.718.
        ///</summary>
        public readonly static JsNumber E;
        ///<summary>
        ///Returns the natural logarithm of 2.
        ///</summary>
        public readonly static JsNumber LN2;
        ///<summary>
        ///Returns the natural logarithm of 10.
        ///</summary>
        public readonly static JsNumber LN10;
        ///<summary>
        ///Returns the base-2 logarithm of e, Euler's number.
        ///</summary>
        public readonly static JsNumber LOG2E;
        ///<summary>
        ///Returns the base-10 logarithm of e, Euler's number.
        ///</summary>
        public readonly static JsNumber LOG10E;
        ///<summary>
        ///Returns the ratio of the circumference of a circle to its diameter, approximately 3.141592653589793.
        ///</summary>
        public readonly static JsNumber PI;
        ///<summary>
        ///Returns the square root of 0.5, or one divided by the square root of 2.
        ///</summary>
        public readonly static JsNumber SQRT1_2;
        ///<summary>
        ///Returns the square root of 2.
        ///</summary>
        public readonly static JsNumber SQRT2;
        ///<summary>
        ///Returns the absolute value of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the absolute value is needed</param>
        ///<returns>The absolute value of the number argument</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber abs(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the arccosine of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the arccosine is needed.</param>
        ///<returns>the arccosine of the number argument.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber acos(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the arcsine of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the arcsine is needed.</param>
        ///<returns>The arcsine of its numeric argument.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber asin(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the arctangent of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the arctangent is needed.</param>
        ///<returns>The return value is the arctangent of its numeric argument.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber atan(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the angle (in radians) from the X axis to a point (y,x).
        ///</summary>
        ///<param name="y">A numeric expression representing the cartesian x-coordinate.</param>
        ///<param name="x">A numeric expression representing the cartesian y-coordinate.</param>
        ///<returns>The return value is between -pi and pi, representing the angle of the supplied (y,x) point.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber atan2(JsNumber y, JsNumber x) { return default(JsNumber); }
        ///<summary>
        ///Returns the smallest integer greater than or equal to its numeric argument.
        ///</summary>
        ///<param name="number">A numeric expression.</param>
        ///<returns>An integer value equal to the smallest integer greater than or equal to its numeric argument.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber ceil(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the cosine of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the cosine is needed.</param>
        ///<returns>The return value is the cosine of its numeric argument.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber cos(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns e (the base of natural logarithms) raised to a power.
        ///</summary>
        ///<param name="number">A numeric expression representing the power of e.</param>
        ///<returns>The return value is a number. The constant e is Euler's number, approximately equal to 2.71828 and number is the supplied argument.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber exp(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the greatest integer less than or equal to its numeric argument.
        ///</summary>
        ///<param name="number">A numeric expression.</param>
        ///<returns>An integer value equal to the greatest integer less than or equal to its numeric argument.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber floor(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the natural logarithm of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the natural logarithm is sought.</param>
        ///<returns>The return value is the natural logarithm of number. The base is e.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber log(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the greater of zero or more supplied numeric expressions.
        ///</summary>
        ///<param name="numbers">Numeric expressions to be evaluated</param>
        ///<returns>If no arguments are provided, the return value is equal to NEGATIVE_INFINITY. If any argument is NaN, the return value is also NaN.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber max(params JsNumber[] numbers) { return default(JsNumber); }
        ///<summary>
        ///Returns the lesser of zero or more supplied numeric expressions.
        ///</summary>
        ///<param name="numbers">Numeric expressions to be evaluated</param>
        ///<returns>If no arguments are provided, the return value is equal to NEGATIVE_INFINITY. If any argument is NaN, the return value is also NaN.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber min(params JsNumber[] numbers) { return default(JsNumber); }
        ///<summary>
        ///Returns the value of a base expression taken to a specified power.
        ///</summary>
        ///<param name="base">The base value of the expression.</param>
        ///<param name="exponent">The exponent value of the expression.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber pow(JsNumber @base, JsNumber exponent) { return default(JsNumber); }
        ///<summary>
        ///Returns a pseudorandom number between 0 and 1.
        ///</summary>
        ///<returns>The pseudorandom number generated is from 0 (inclusive) to 1 (exclusive), that is, the returned number can be zero, but it will always be less than one.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber random() { return default(JsNumber); }
        ///<summary>
        ///Returns a supplied numeric expression rounded to the nearest integer.
        ///</summary>
        ///<param name="number">The value to be rounded to the nearest integer.</param>
        ///<returns>For positive numbers, if the decimal portion of number is 0.5 or greater, 
        ///the return value is equal to the smallest integer greater than number. If the 
        ///decimal portion is less than 0.5, the return value is the largest integer less than 
        ///or equal to number. For negative numbers, if the decimal portion is exactly -0.5, 
        ///the return value is the smallest integer that is greater than the number.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber round(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the sine of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the sine is needed</param>
        ///<returns>The sine of the numeric argument</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber sin(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the square root of a number.
        ///</summary>
        ///<param name="number">A numeric expression.</param>
        ///<returns>If number is negative, the return value is NaN.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber sqrt(JsNumber number) { return default(JsNumber); }
        ///<summary>
        ///Returns the tangent of a number.
        ///</summary>
        ///<param name="number">A numeric expression for which the tangent is sought.</param>
        ///<returns>The tangent of number.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsNumber tan(JsNumber number) { return default(JsNumber); }
    }
    #endregion
    #region JsNumber
    ///<summary>
    ///An object representation of the number data type and placeholder for numeric constants.
    ///</summary>
    [JsType(JsMode.Prototype, Export = false, Name = "Number", NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsNumber : JsObjectBase, IConvertible, IComparable, IComparable<JsNumber>, IEquatable<JsNumber>, IFormattable
    {
        double _Value;
        public JsNumber(double d)
        {
            _Value = d;
        }
        public static implicit operator double(JsNumber number) { return default(double); }
        public static implicit operator decimal(JsNumber number) { return default(decimal); }
        public static implicit operator int(JsNumber number) { return default(int); }
        public static implicit operator float(JsNumber number) { return default(float); }
        public static implicit operator JsNumber(decimal d) { return default(JsNumber); }
        public static implicit operator JsNumber(double d) { return default(JsNumber); }
        public static implicit operator JsNumber(sbyte d) { return default(JsNumber); }
        public static implicit operator JsNumber(int d) { return default(JsNumber); }
        public static JsBoolean operator ==(JsNumber x, JsNumber y) { return new JsBoolean(x._Value == y._Value); }
        public static JsBoolean operator !=(JsNumber x, JsNumber y) { return default(JsBoolean); }
        public static JsNumber operator ++(JsNumber x) { return default(JsNumber); }
        public static JsNumber operator --(JsNumber x) { return default(JsNumber); }
        public static JsNumber operator *(JsNumber x, JsNumber y) { return default(JsNumber); }
        public static JsNumber operator /(JsNumber x, JsNumber y) { return default(JsNumber); }
        public static JsBoolean operator >(JsNumber x, JsNumber y) { return new JsBoolean(x._Value == y._Value); }
        public static JsBoolean operator >=(JsNumber x, JsNumber y) { return new JsBoolean(x._Value == y._Value); }
        public static JsBoolean operator <(JsNumber x, JsNumber y) { return new JsBoolean(x._Value == y._Value); }
        public static JsBoolean operator <=(JsNumber x, JsNumber y) { return new JsBoolean(x._Value == y._Value); }
        //[EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return default(bool); }
        //[EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return default(int); }
        //[EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return _Value.ToString(); }
        int IComparable.CompareTo(object obj) { return default(int); }
        static int Compare(JsNumber x, JsNumber y) { return default(int); }
        TypeCode IConvertible.GetTypeCode() { return default(TypeCode); }
        bool IConvertible.ToBoolean(IFormatProvider provider) { return default(bool); }
        byte IConvertible.ToByte(IFormatProvider provider) { return default(byte); }
        char IConvertible.ToChar(IFormatProvider provider) { return default(char); }
        DateTime IConvertible.ToDateTime(IFormatProvider provider) { return default(DateTime); }
        decimal IConvertible.ToDecimal(IFormatProvider provider) { return default(decimal); }
        double IConvertible.ToDouble(IFormatProvider provider) { return default(double); }
        short IConvertible.ToInt16(IFormatProvider provider) { return default(short); }
        int IConvertible.ToInt32(IFormatProvider provider) { return default(int); }
        long IConvertible.ToInt64(IFormatProvider provider) { return default(long); }
        sbyte IConvertible.ToSByte(IFormatProvider provider) { return default(sbyte); }
        float IConvertible.ToSingle(IFormatProvider provider) { return default(float); }
        string IConvertible.ToString(IFormatProvider provider) { return default(string); }
        object IConvertible.ToType(Type type, IFormatProvider provider) { return default(object); }
        ushort IConvertible.ToUInt16(IFormatProvider provider) { return default(ushort); }
        uint IConvertible.ToUInt32(IFormatProvider provider) { return default(uint); }
        ulong IConvertible.ToUInt64(IFormatProvider provider) { return default(ulong); }
        int IComparable<JsNumber>.CompareTo(JsNumber other) { return default(int); }
        bool IEquatable<JsNumber>.Equals(JsNumber other) { return default(bool); }
        string IFormattable.ToString(string format, IFormatProvider formatProvider) { return default(string); }
        ///<summary>
        ///Returns the largest number representable in JScript. Equal to approximately 1.79E+308.
        ///</summary>
        public readonly static JsNumber MAX_VALUE;
        ///<summary>
        ///Returns the number closest to zero representable in JScript. Equal to approximately 5.00E-324.
        ///</summary>
        public readonly static JsNumber MIN_VALUE;
        ///<summary>
        ///A special value that indicates an arithmetic expression returned a value that was not a number.
        ///</summary>
        public readonly static JsNumber NaN;
        ///<summary>
        ///Returns a value more negative than the largest negative number.
        ///</summary>
        public readonly static JsNumber NEGATIVE_INFINITY;
        ///<summary>
        ///Returns a value larger than the largest number.
        ///</summary>
        public readonly static JsNumber POSITIVE_INFINITY;
        ///<summary>
        ///Returns a JsString containing a number represented in exponential notation.
        ///</summary>
        ///<param name="fractionDigits">Number of digits after the decimal point. Must be in the range 0 – 20, inclusive.</param>
        ///<returns>A JsString representation of a number in exponential notation. The JsString contains one digit before the significand's decimal point, and may contain fractionDigits digits after it. If fractionDigits is not supplied, the toExponential method returns as many digits necessary to uniquely specify the number.</returns>
        public JsString toExponential(int fractionDigits) { return default(JsString); }
        ///<summary>
        ///Returns a JsString containing a number represented in exponential notation.
        ///</summary>
        ///<returns>A JsString representation of a number in exponential notation. The JsString contains one digit before the significand's decimal point, and may contain fractionDigits digits after it. If fractionDigits is not supplied, the toExponential method returns as many digits necessary to uniquely specify the number.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toExponential() { return default(JsString); }
        ///<summary>
        ///Returns a JsString containing a number represented either in exponential or fixed-point notation with a specified number of digits.
        ///</summary>
        ///<param name="precision">Number of significant digits. Must be in the range 1 – 21, inclusive.</param>
        ///<returns>For numbers in exponential notation, precision - 1 digits are returned after the decimal point. For numbers in fixed notation, precision significant digits are returned.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toPrecision(int precision) { return default(JsString); }
        ///<summary>
        ///Returns a JsString containing a number represented either in exponential or fixed-point notation with a specified number of digits.
        ///</summary>
        ///<returns>For numbers in exponential notation, precision - 1 digits are returned after the decimal point. For numbers in fixed notation, precision significant digits are returned.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toPrecision() { return default(JsString); }
        ///<summary>
        ///Returns a JsString representation of an object.
        ///</summary>
        ///<returns>Returns the textual representation of the number.</returns>
        [JsMethod(NativeOverloads = true)]
        public new JsString toString() { return default(JsString); }
        ///<summary>
        ///Returns a JsString representation of an object.
        ///</summary>
        ///<param name="radix">Specifies a radix for converting numeric values to strings. This value is only used for numbers.</param>
        ///<returns>Returns the textual representation of the number.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toString(int radix) { return default(JsString); }
        ///<summary>
        ///Returns the primitive value of the specified object.
        ///</summary>
        ///<returns>The numeric value.</returns>
        [JsMethod(NativeOverloads = true)]
        public new double valueOf() { return default(double); }
        ///<summary>
        ///The toFixed() method formats a number to use a specified number of trailing decimals.
        ///The number is rounded up, and nulls are added after the decimal point (if needed), to create the desired decimal length.
        ///</summary>
        ///<returns></returns>
        public JsString toFixed() { return default(JsString); }
        ///<summary>
        ///The toFixed() method formats a number to use a specified number of trailing decimals.
        ///The number is rounded up, and nulls are added after the decimal point (if needed), to create the desired decimal length.
        ///</summary>
        ///<param name="x">The number of digits after the decimal point. Default is 0 (no digits after the decimal point)</param>
        ///<returns></returns>
        public JsString toFixed(int x) { return default(JsString); }
    }
    #endregion
    #region JsObject
    [JsType(JsMode.Prototype, Export = false)]
    public abstract partial class JsObjectBase
    {
        ///<summary>
        ///Watches for a property to be assigned a value and runs a function when that occurs.
        ///Watches for assignment to a property named prop in this object, calling handler(prop, oldval, newval) whenever prop is set and storing the return value in that property. A watchpoint can filter (or nullify) the value assignment, by returning a modified newval (or by returning oldval).
        ///If you delete a property for which a watchpoint has been set, that watchpoint does not disappear. If you later recreate the property, the watchpoint is still in effect.
        ///To remove a watchpoint, use the unwatch method. By default, the watch method is inherited by every object descended from Object.
        ///The JavaScript debugger has functionality similar to that provided by this method, as well as other debugging options. For information on the debugger, see Venkman.
        ///In NES 3.0 and 4.x, handler is called from native code as well as assignments in script. In Firefox, handler is only called from assignments in script, not from native code. For example, window.watch('location', myHandler) will not call myHandler if the user clicks a link to an anchor within the current document. However, window.location += '#myAnchor' will call myHandler:
        ///</summary>
        ///<param name="prop">The name of a property of the object.</param>
        ///<param name="handler">A function to call.</param>
        //[SupportedBrowsers(BrowserTypes.FireFox2 | BrowserTypes.FireFox3 | BrowserTypes.FireFox3_5 | BrowserTypes.FireFox4)]
        public virtual void watch(JsString prop, Action handler) { }
        ///<summary>
        ///Removes a watchpoint set with the watch method
        ///The JavaScript debugger has functionality similar to that provided by this method, as well as other debugging options. For information on the debugger, see Venkman.
        ///By default, this method is inherited by every object descended from Object.
        ///</summary>
        ///<param name="prop">The name of a property of the object.</param>
        //[SupportedBrowsers(BrowserTypes.FireFox2 | BrowserTypes.FireFox3 | BrowserTypes.FireFox3_5 | BrowserTypes.FireFox4)]
        public virtual void unwatch(JsString prop) { }
        ///<summary>
        ///Returns the primitive value of the specified object.
        ///The required object reference is any intrinsic JScript object.
        ///The valueOf method is defined differently for each intrinsic JScript object.
        ///Object
        ///Return Value
        ///Array
        ///Returns the array instance.
        ///Boolean
        ///The Boolean value.
        ///Date
        ///The stored time value in milliseconds since midnight, January 1, 1970 UTC.
        ///Function
        ///The function itself.
        ///Number
        ///The numeric value.
        ///Object
        ///The object itself. This is the default.
        ///String
        ///The JsString value.
        ///The Math and Error objects do not have a valueOf method.
        ///</summary>
        ///<returns></returns>
        public virtual object valueOf() { return default(object); }

        public JsFunction constructor { get; set; }

        ///<summary>
        ///Returns a Boolean value indicating whether an object exists in another object's prototype chain.
        ///</summary>
        ///<param name="object2">Another object whose prototype chain is to be checked.</param>
        ///<returns>true if object2 has object1 in its prototype chain. The prototype chain is used to share functionality between instances of the same object type. The isPrototypeOf method returns false when object2 is not an object or when object1 does not appear in the prototype chain of the object2.</returns>
        public virtual JsBoolean isPrototypeOf(object object2) { return default(JsBoolean); }
        ///<summary>
        ///Returns a Boolean value indicating whether an object has a property with the specified name.
        ///</summary>
        ///<param name="proName">String value of a property name.</param>
        ///<returns> true if object has a property of the specified name, false if it does not. This method does not check if the property exists in the object's prototype chain; the property must be a member of the object itself.</returns>
        public virtual JsBoolean hasOwnProperty(JsString proName) { return default(JsBoolean); }
        ///<summary>
        ///Returns a JsString representation of an object.
        ///</summary>
        ///<returns>Returns "[object objectname]", where objectname is the name of the object type.</returns>
        public virtual JsString toString() { return default(JsString); }

        #region Object Hiding
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return base.ToString(); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool Equals(object objA, object objB) { return object.Equals(objA, objB); }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool ReferenceEquals(object objA, object objB) { return object.ReferenceEquals(objA, objB); }
        #endregion
    }
    [JsType(JsMode.Prototype, Export = false, Name = "Object", NativeEnumerator = true, NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsObject : JsObjectBase, IEnumerable<JsString>
    {
        public JsObject() { }
        public JsObject(object json) { }
        [JsProperty(NativeIndexer = true)]
        public object this[JsString name] { get { return default(object); } set { } }
        IEnumerator<JsString> IEnumerable<JsString>.GetEnumerator() { return default(IEnumerator<JsString>); }
        IEnumerator IEnumerable.GetEnumerator() { return default(IEnumerator); }
        [JsMethod(Name = "", OmitParanthesis = true, InsertArg0 = "[", InsertArg1 = "] = ", OmitCommas = true)]
        public void Add(JsString name, object value) { }

    }
    #endregion
    #region JsObject<T>
    [JsType(JsMode.Prototype, Export = false, Name = "Object", NativeEnumerator = true, NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsObject<T> : JsObjectBase, IEnumerable<JsString>
    {
        public static implicit operator JsObject(JsObject<T> obj) { return null; }
        [JsMethod(IgnoreGenericArguments = true)]
        public JsObject() { }
        [JsProperty(NativeIndexer = true)]
        public T this[JsString name] { get { return default(T); } set { } }
        IEnumerator<JsString> IEnumerable<JsString>.GetEnumerator() { return default(IEnumerator<JsString>); }
        IEnumerator IEnumerable.GetEnumerator() { return default(IEnumerator); }
        [JsMethod(Name = "", OmitParanthesis = true, InsertArg0 = "[", InsertArg1 = "] = ", OmitCommas = true)]
        public void Add(JsString name, T value) { }
    }
    #endregion
    #region JsObject<K, T>

    [JsType(JsMode.Prototype, Export = false, Name = "Object", IgnoreGenericTypeArguments = true, NativeEnumerator = true, NativeCasts = true, NativeOperatorOverloads = true)]
    public class JsObject<K, T> : JsObjectBase, IEnumerable<K>
    {
        public static implicit operator JsObject(JsObject<K, T> obj) { return null; }
        [JsMethod(IgnoreGenericArguments = true)]
        public JsObject() { }
        [JsProperty(NativeIndexer = true)]
        public T this[K key] { get { return default(T); } set { } }
        IEnumerator<K> IEnumerable<K>.GetEnumerator() { return default(IEnumerator<K>); }
        IEnumerator IEnumerable.GetEnumerator() { return default(IEnumerator); }
        [JsMethod(Name = "", OmitParanthesis = true, InsertArg0 = "[", InsertArg1 = "] = ", OmitCommas = true)]
        public void Add(K name, T value) { }
    }
    #endregion
    #region JsRegExp
    ///<summary>
    ///An object that contains a regular expression pattern along with flags that identify how to apply the pattern.
    ///re = /pattern/[flags]
    ///re = new RegExp("pattern"[,"flags"]) 
    ///</summary>
    [JsType(JsMode.Prototype, Export = false, Name = "RegExp")]
    public partial class JsRegExp : JsObjectBase
    {
        public JsRegExp(JsString pattern) { }
        public JsRegExp(JsString pattern, JsString flags) { }
        [JsMethod(NativeOverloads = true)]
        public JsBoolean test(JsString ch) { return default(JsBoolean); }
        ///<summary>
        ///If the exec method does not find a match, it returns null. If it finds a match, exec returns an array, and the properties of the global RegExp object are updated to reflect the results of the match. Element zero of the array contains the entire match, while elements 1 – n contain any submatches that have occurred within the match. This behavior is identical to the behavior of the match method without the global flag (g) set. 
        ///If the global flag is set for a regular expression, exec searches the JsString beginning at the position indicated by the value of lastIndex. If the global flag is not set, exec ignores the value of lastIndex and searches from the beginning of the JsString. 
        ///</summary>
        ///<param name="text">The String object or JsString literal on which to perform the search</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsRegExpResult exec(JsString text) { return default(JsRegExpResult); }
    }
    #endregion
    #region JsRegExpResult
    [JsType(JsMode.Prototype, Name = "Array", NativeEnumerator = false, Export = false, IgnoreGenericTypeArguments = true, NativeArrayEnumerator = true, NativeCasts = true)]
    public partial class JsRegExpResult : JsArray<JsString>
    {
        ///<summary>
        ///Returns the JsString against which a regular expression search was performed. Read-only.
        ///</summary>
        public readonly JsString input;
        ///<summary>
        ///Returns the character position where the first successful match begins in a searched JsString. Read-only.
        ///</summary>
        public readonly JsNumber index;
        ///<summary>
        ///Returns the character position where the next match begins in a searched JsString.
        ///</summary>
        public readonly JsNumber lastIndex;
        ///<summary>
        ///Returns the last matched characters from any regular expression search. Read-only.
        ///</summary>
        public readonly JsString lastMatch;
        ///<summary>
        ///Returns the last parenthesized submatch from any regular expression search, if any. Read-only.
        ///</summary>
        public readonly JsString lastParen;
        ///<summary>
        ///Returns the characters from the beginning of a searched JsString up to the position before the beginning of the last match. Read-only.
        ///</summary>
        public readonly JsString leftContext;
        ///<summary>
        ///Returns the characters from the position following the last match to the end of the searched JsString. Read-only.
        ///</summary>
        public readonly JsString rightContext;
    }
    #endregion
    #region JsString
    ///<summary>
    ///Allows manipulation and formatting of text strings and determination and location of substrings within strings.
    ///</summary>
    [JsType(JsMode.Prototype, Export = false, Name = "String", NativeEnumerator = false, NativeArrayEnumerator = true, NativeCasts = true, NativeOperatorOverloads = true)]
    public partial class JsString : JsObjectBase
    {
        public JsString() { }
        public JsString(object obj)
        {
            if (obj != null)
                _Value = obj.ToString();
        }
        string _Value;
        public static implicit operator JsString(string s)
        {
            return new JsString(s);
        }
        public static implicit operator string(JsString s)
        {
            return s._Value;
        }
        public static JsString operator +(JsString x, JsString y)
        {
            return new JsString(x._Value + y._Value);
        }
        public static JsString operator +(JsString x, string y)
        {
            return new JsString(x._Value + y);
        }
        public override string ToString()
        {
            return _Value;
        }
        public static JsString operator +(string x, JsString y) { return default(JsString); }
        public static bool operator >(JsString x, JsString y) { return false; }
        public static bool operator <(JsString x, JsString y) { return false; }
        ///<summary>
        ///Returns the last occurrence of a substring within a String object.
        ///</summary>
        ///<param name="sub">The char to search for within the String object.</param>
        ///<returns> an integer value indicating the beginning of the substring within the String object. If the substring is not found, a -1 is returned. If startindex is negative, startindex is treated as zero. If it is larger than the greatest character position index, it is treated as the largest possible index.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber lastIndexOf(char sub) { return default(JsNumber); }
        ///<summary>
        ///Returns the last occurrence of a substring within a String object.
        ///</summary>
        ///<param name="sub">The string to search for within the String object.</param>
        ///<returns> an integer value indicating the beginning of the substring within the String object. If the substring is not found, a -1 is returned. If startindex is negative, startindex is treated as zero. If it is larger than the greatest character position index, it is treated as the largest possible index.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber lastIndexOf(JsString sub) { return default(JsNumber); }
        ///<summary>
        ///Returns the last occurrence of a substring within a String object.
        ///</summary>
        ///<param name="substring">The substring to search for within the String object.</param>
        ///<param name="startindex">Integer value specifying the index to begin searching within the String object. If omitted, searching begins at the end of the string.</param>
        ///<returns> an integer value indicating the beginning of the substring within the String object. If the substring is not found, a -1 is returned. If startindex is negative, startindex is treated as zero. If it is larger than the greatest character position index, it is treated as the largest possible index.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber lastIndexOf(JsString substring, JsNumber startindex) { return default(JsNumber); }
        /// <summary>
        /// Returns the substring at the specified location within a String object.
        /// </summary>
        /// <param name="start">The zero-based index integer indicating the beginning of the substring.</param>
        /// <returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsString substring(JsNumber start) { return default(JsString); }
        /// <summary>
        /// Returns the substring at the specified location within a String object.
        /// </summary>
        /// <param name="start">The zero-based index integer indicating the beginning of the substring.</param>
        /// <param name="end">The zero-based index integer indicating the end of the substring. The substring includes the characters up to, but not including, the character indicated by end. If end is omitted, the characters from start through the end of the original string are returned.</param>
        /// <returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsString substring(JsNumber start, JsNumber end) { return default(JsString); }
        /// <summary>
        /// Returns a substring beginning at a specified location and having a specified length.
        /// </summary>
        /// <param name="start">The starting position of the desired substring. The index of the first character in the string is zero.</param>
        /// <param name="length">The number of characters to include in the returned substring.</param>
        /// <returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsString substr(JsNumber start, JsNumber length) { return default(JsString); }
        /// <summary>
        /// Returns a substring beginning at a specified location and having a specified length.
        /// </summary>
        /// <param name="start">The starting position of the desired substring. The index of the first character in the string is zero.</param>
        /// <returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsString substr(JsNumber start) { return default(JsString); }
        [JsMethod(NativeOverloads = true)]
        public JsArray<JsString> split(JsRegExp re) { return default(JsArray<JsString>); }
        [JsMethod(NativeOverloads = true)]
        public JsArray<JsString> split(char sep) { return default(JsArray<JsString>); }
        [JsMethod(NativeOverloads = true)]
        public JsArray<JsString> split(JsString sep) { return default(JsArray<JsString>); }
        ///<summary>
        ///Returns the character position where the first occurrence of a substring occurs within a String object.
        ///</summary>
        ///<param name="subString">Substring to search for within the String object.</param>
        ///<returns>An integer value indicating the beginning of the substring within the String object. If the substring is not found, a -1 is returned.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber indexOf(JsString subString) { return default(JsNumber); }
        ///<summary>
        ///Returns the character position where the first occurrence of a substring occurs within a String object.
        ///</summary>
        ///<param name="subString">Substring to search for within the String object.</param>
        ///<param name="startIndex">Integer value specifying the index to begin searching within the String object. If omitted, searching starts at the beginning of the string.</param>
        ///<returns>An integer value indicating the beginning of the substring within the String object. If the substring is not found, a -1 is returned.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber indexOf(JsString subString, JsNumber startIndex) { return default(JsNumber); }
        ///<summary>
        ///Returns the position of the first substring match in a regular expression search.
        ///</summary>
        ///<param name="regExp">An instance of a Regular Expression object containing the regular expression pattern and applicable flags.</param>
        ///<returns>The search method indicates if a match is present or not. If a match is found, the search method returns an integer value that indicates the offset from the beginning of the string where the match occurred. If no match is found, it returns -1.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber search(JsRegExp regExp) { return default(JsNumber); }
        ///<summary>
        ///Returns the position of the first substring match in a regular expression search.
        ///</summary>
        ///<param name="searchText">The String object or string literal on which to perform the search.</param>
        ///<returns>The search method indicates if a match is present or not. If a match is found, the search method returns an integer value that indicates the offset from the beginning of the string where the match occurred. If no match is found, it returns -1.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsNumber search(string searchText) { return default(JsNumber); }
        ///<summary>
        ///Returns a copy of a string with text replaced using a regular expression or search string. 
        ///</summary>
        ///<param name="regExp">An instance of a Regular Expression object containing the regular expression pattern and applicable flags. </param>
        ///<param name="replaceText">Required. A String object or string literal containing the text to replace for every successful match of rgExp in stringObj. In JScript 5.5 or later, the replaceText argument can also be a function that returns the replacement text. </param>
        ///<returns>The result of the replace method is a copy of stringObj after the specified replacements have been made. </returns>
        public JsString replace(JsRegExp regExp, string replaceText) { return default(JsString); }
        ///<summary>
        ///Returns a copy of a string with text replaced using a regular expression or search string. 
        ///</summary>
        ///<param name="searchText">String object or literal that is converted to a string, and an exact search is made for the results; no attempt is made to convert the string into a regular expression. </param>
        ///<param name="replaceText">Required. A String object or string literal containing the text to replace for every successful match of rgExp in stringObj. In JScript 5.5 or later, the replaceText argument can also be a function that returns the replacement text. </param>
        ///<returns>The result of the replace method is a copy of stringObj after the specified replacements have been made. </returns>
        public JsString replace(string searchText, string replaceText) { return default(string); }

        ///<summary>
        ///Returns a copy of a string with text replaced using a regular expression or search string. 
        ///Replace function with no params
        ///</summary>
        ///<param name="regExp">An instance of a Regular Expression object containing the regular expression pattern and applicable flags. </param>
        ///<param name="replaceFunction">You can specify a function as the second parameter. In this case, the function will be invoked after the match has been performed. The function's result (return value) will be used as the replacement string. (Note: the above-mentioned special replacement patterns do not apply in this case.) Note that the function will be invoked multiple times for each full match to be replaced if the regular expression in the first parameter is global.</param>
        ///<returns>The result of the replace method is a copy of stringObj after the specified replacements have been made. </returns>
        public JsString replace(JsRegExp regExp, JsFunc<JsString, JsNumber, JsString, JsString> replaceFunction) { return default(JsString); }
        ///<summary>
        ///Returns a copy of a string with text replaced using a regular expression or search string. 
        ///Replace function with no params
        ///</summary>
        ///<param name="searchText">String object or literal that is converted to a string, and an exact search is made for the results; no attempt is made to convert the string into a regular expression. </param>
        ///<param name="replaceFunction">You can specify a function as the second parameter. In this case, the function will be invoked after the match has been performed. The function's result (return value) will be used as the replacement string. (Note: the above-mentioned special replacement patterns do not apply in this case.) Note that the function will be invoked multiple times for each full match to be replaced if the regular expression in the first parameter is global.</param>
        ///<returns>The result of the replace method is a copy of stringObj after the specified replacements have been made. </returns>
        public JsString replace(string searchText, JsFunc<JsString, JsNumber, JsString, JsString> replaceFunction) { return default(string); }

        /// <summary>
        /// The trim method returns the string stripped of whitespace from both ends. trim does not affect the value of the string itself.
        /// Supported in Firefox, Chrome, IE 9, Opera 10.5 and Safari 5
        /// (Description from MDN)
        /// </summary>
        public JsString trim() { return default(JsString); }

        /// <summary>
        ///Replace function with 1 param
        /// </summary>
        public JsString replace(string searchText, JsFunc<JsString, JsNumber, JsString, JsString, JsString> replaceFunction) { return default(string); }
        /// <summary>
        ///Replace function with 2 params
        /// </summary>
        public JsString replace(string searchText, JsFunc<JsString, JsNumber, JsString, JsString, JsString, JsString> replaceFunction) { return default(string); }
        /// <summary>
        ///Replace function with 3 params
        /// </summary>
        public JsString replace(string searchText, JsFunc<JsString, JsNumber, JsString, JsString, JsString, JsString, JsString> replaceFunction) { return default(string); }
        /// <summary>
        ///Replace function with 1 params
        /// </summary>
        public JsString replace(JsRegExp regExp, JsFunc<JsString, JsNumber, JsString, JsString, JsString> replaceFunction) { return default(string); }
        /// <summary>
        ///Replace function with 2 params
        /// </summary>
        public JsString replace(JsRegExp regExp, JsFunc<JsString, JsNumber, JsString, JsString, JsString, JsString> replaceFunction) { return default(string); }
        /// <summary>
        ///Replace function with 3 params
        /// </summary>
        public JsString replace(JsRegExp regExp, JsFunc<JsString, JsNumber, JsString, JsString, JsString, JsString, JsString> replaceFunction) { return default(string); }

        ///<summary>
        ///Returns the length of a String object.
        ///</summary>
        public readonly JsNumber length;
        ///<summary>
        ///Places an HTML anchor with a NAME attribute around specified text in the object.
        ///</summary>
        ///<param name="anchorString">Text you want to place in the NAME attribute of an HTML anchor.</param>
        [JsMethod(NativeOverloads = true)]
        public void anchor(string anchorString) { }
        ///<summary>
        ///Places HTML &lt;BIG&gt; tags around text in a String object.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public JsString big() { return default(JsString); }
        ///<summary>
        ///Places HTML &lt;BLINK&gt; tags around text in a String object.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public JsString blink() { return default(JsString); }
        ///<summary>
        ///Places HTML &lt;B&gt; tags around text in a String object.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public JsString bold() { return default(JsString); }
        ///<summary>
        ///Returns the character at the specified index.
        ///</summary>
        ///<param name="index">Zero-based index of the desired character. Valid values are between 0 and the length of the string minus 1.</param>
        [JsMethod(NativeOverloads = true)]
        public JsString charAt(JsNumber index) { return default(JsString); }
        ///<summary>
        ///Returns an integer representing the Unicode encoding of the character at the specified location.
        ///</summary>
        ///<param name="index">Zero-based index of the desired character. Valid values are between 0 and the length of the string minus 1.</param>
        [JsMethod(NativeOverloads = true)]
        public JsNumber charCodeAt(JsNumber index) { return default(JsNumber); }
        ///<summary>
        ///Returns a string value containing the concatenation of two or more supplied strings.
        ///</summary>
        ///<param name="strings">String objects or literals to concatenate to the end of string1.</param>
        ///<returns>The result of the concat method is equivalent to: result = string1 + string2 + string3 + + stringN. A change of value in either a source or result string does not affect the value in the other string. If any of the arguments are not strings, they are first converted to strings before being concatenated to string1.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString concat(params JsString[] strings) { return default(JsString); }
        ///<summary>
        ///Places HTML &lt;TT&gt; tags around text in a String object.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public string @fixed() { return default(string); }
        ///<summary>
        ///Places an HTML &lt;FONT&gt; tag with the COLOR attribute around the text in a String object.
        ///</summary>
        [JsMethod(NativeOverloads = true)]
        public JsString fontcolor() { return default(JsString); }
        ///<summary>
        ///Places an HTML &lt;FONT&gt; tag with the SIZE attribute around the text in a String object.
        ///</summary>
        ///<param name="size">Integer value that specifies the size of the text.</param>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public string fontsize(JsNumber size) { return default(string); }
        ///<summary>
        ///Returns a string from a number of Unicode character values.
        ///</summary>
        ///<param name="charCodes">A series of Unicode character values to convert to a string.</param>
        ///<returns>If no arguments are supplied, the result is the empty string.</returns>
        [JsMethod(NativeOverloads = true)]
        public static JsString fromCharCode(params JsNumber[] charCodes) { return default(JsString); }
        ///<summary>
        ///Places HTML &lt;I&gt; tags around text in a String object.
        ///</summary>
        ///<returns></returns>
        [JsMethod(NativeOverloads = true)]
        public JsString italics() { return default(JsString); }
        ///<summary>
        ///Returns a string where all alphabetic characters have been converted to lowercase, taking into account the host environment's current locale.
        ///</summary>
        ///<returns>Converts the characters in a string, taking into account the host environment's current locale. In most cases, the results are the same as you would obtain with the toLowerCase method. Results differ if the rules for a language conflict with the regular Unicode case mappings.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toLocaleLowerCase() { return default(JsString); }
        ///<summary>
        ///Returns a string where all alphabetic characters have been converted to uppercase, taking into account the host environment's current locale.
        ///</summary>
        ///<returns>Converts the characters in a string, taking into account the host environment's current locale. In most cases, the results are the same as you would obtain with the toUpperCase method. Results differ if the rules for a language conflict with the regular Unicode case mappings.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toLocaleUpperCase() { return default(JsString); }
        ///<summary>
        ///Returns a string where all alphabetic characters have been converted to lowercase.
        ///</summary>
        ///<returns>The method has no effect on nonalphabetic characters.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toLowerCase() { return default(JsString); }
        ///<summary>
        ///Returns a string where all alphabetic characters have been converted to uppercase.
        ///</summary>
        ///<returns>The method has no effect on non-alphabetic characters.</returns>
        [JsMethod(NativeOverloads = true)]
        public JsString toUpperCase() { return default(JsString); }

        /// <summary>
        /// Executes a search on a string using a regular expression pattern, and returns an array containing the results of that search.
        /// </summary>
        /// <param name="rgExp">Required. An instance of a Regular Expression object containing the regular expression pattern and applicable flags. </param>
        /// <returns></returns>
        public JsRegExpResult match(JsRegExp rgExp) { return null; }
        /// <summary>
        /// Executes a search on a string using a regular expression pattern, and returns an array containing the results of that search.
        /// </summary>
        /// <param name="rgExp">Required. A string literal containing the regular expression pattern and flags. </param>
        /// <returns></returns>
        public JsRegExpResult match(JsString rgExp) { return null; }

        /// <summary>
        /// Returns a section of a string.
        /// </summary>
        /// <returns></returns>
        public JsString slice(int start, int end) { return null; }
        /// <summary>
        /// Returns a section of a string.
        /// </summary>
        /// <returns></returns>
        public JsString slice(int start) { return null; }

        /// <summary>
        /// Performs a locale-aware string comparison
        /// </summary>
        /// <param name="compareString">The string to compare to</param>
        /// <param name="locales">An optional array of BCP 47 language codes (see remarks)</param>
        /// <param name="options">Optional options (see remarks)</param>
        /// <returns>a negative number if the string sorts earlier than compareString,  
        /// a positive number if it sorts afterwards, and 0 if they are the same.</returns>
        /// <remarks>
        /// A list of language codes can be found <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Intl#Locale_identification_and_negotiation">here</a>.
        /// A description of valid options can be found <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/localeCompare">here</a>.
        /// </remarks>
        [JsMethod(OmitOptionalParameters = true)]
        public JsNumber localeCompare(JsString compareString, JsArray<JsString> locales = null, object options = null) { return 0; }
    }
    /// <summary>
    /// A special class, when used as a method parameter, can be assigned as string, and generates the native js code inside the string
    /// </summary>
    [JsType(JsMode.Prototype, Export = false, NativeOperatorOverloads = true)]
    public partial class JsCode
    {
        protected JsCode() { }
        public static implicit operator JsCode(string s) { return default(JsCode); }
        public static implicit operator JsCode(JsString s) { return default(JsCode); }
    }

    #endregion

    #region Delegates

    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsAction();
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsAction<in T>(T arg);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsAction<in T1, in T2>(T1 arg1, T2 arg2);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsAction<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsAction<in T1, in T2, in T3, in T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsAction<in T1, in T2, in T3, in T4, in T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsAction<in T1, in T2, in T3, in T4, in T5, in T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate TResult JsFunc<out TResult>();
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsDelegate(NativeDelegates = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate TResult JsFunc<in T, out TResult>(T arg);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeDelegates = true)]
    public delegate TResult JsFunc<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeDelegates = true)]
    public delegate TResult JsFunc<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeDelegates = true)]
    public delegate TResult JsFunc<in T1, in T2, in T3, in T4, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeDelegates = true)]
    public delegate TResult JsFunc<in T1, in T2, in T3, in T4, in T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    /// <summary>
    /// A delegate for native javascript
    /// </summary>
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeDelegates = true)]
    public delegate TResult JsFunc<in T1, in T2, in T3, in T4, in T5, in T6, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);









    [JsDelegate(NativeFunction = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate void JsNativeAction();
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate void JsNativeAction<T>(T arg);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate void JsNativeAction<T1, T2>(T1 arg1, T2 arg2);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate void JsNativeAction<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate void JsNativeAction<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate void JsNativeAction<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate void JsNativeAction<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate TResult JsNativeFunc<TResult>();
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate TResult JsNativeFunc<T, TResult>(T arg);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate TResult JsNativeFunc<T1, T2, TResult>(T1 arg1, T2 arg2);
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    [JsDelegate(NativeFunction = true)]
    public delegate TResult JsNativeFunc<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);
    [JsDelegate(NativeFunction = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate TResult JsNativeFunc<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    [JsDelegate(NativeFunction = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate TResult JsNativeFunc<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    [JsDelegate(NativeFunction = true)]
    [JsType(JsMode.Json, OmitCasts = true, Export = false)]
    public delegate TResult JsNativeFunc<T1, T2, T3, T4, T5, T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    #endregion

    #region JsExtensions
    /// <summary>
    /// Provides extension methods for C# to JavaScript
    /// </summary>
    public static partial class JsExtensions
    {
        /// <summary>
        /// Invisibly converts the string reference to JsString on order to use native javascript string functions
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [JsMethod(OmitCalls = true)]
        public static JsString AsJsString(this string s) { return new JsString(s); }
        ///<summary>
        ///Allows converting an object to a different type without affecting the generated javascript code.
        ///</summary>
        ///<typeparam name="T"></typeparam>
        ///<param name="obj"></param>
        ///<returns></returns>
        [JsMethod(OmitCalls = true)]
        public static T As<T>(this object obj) { return default(T); }
        /// <summary>
        /// A quick shortcut to invisibly cast any list to jsarray (usually for Json mode types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        [JsMethod(OmitCalls = true)]
        public static T[] AsArray<T>(this JsArray<T> list) { return null; }
        /// <summary>
        /// A quick shortcut to invisibly cast any list to jsarray (usually for Json mode types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        [JsMethod(OmitCalls = true)]
        public static List<T> AsList<T>(this JsArray<T> list) { return null; }
        [JsMethod(OmitCalls = true)]
        public static JsArray<T> AsJsArray<T>(this IList<T> list) { return null; }
        /// <summary>
        /// A quick shortcut to invisibly cast any dictionary to JsObject (usually for Json mode types)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        [JsMethod(OmitCalls = true)]
        public static JsObject<T> AsJsObject<T>(this IDictionary<string, T> dic) { return null; }
        /// <summary>
        /// Returns a Boolean value that indicates whether or not an object is an instance of a particular class or constructed function.
        /// This is a JavaScript operator, it is implemented in C# as an extension method.
        /// </summary>
        /// <example>
        /// <code>
        /// var isArray = obj.instanceof&lt;JsArray&gt;()
        /// </code>
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        [JsMethod(OmitDotOperator = true, OmitParanthesis = true, Export = false, NativeOverloads = true, Name = "instanceof", ExtensionImplementedInInstance = true, IgnoreGenericArguments = false)]
        public static bool instanceof<T>(this object obj)
        {
            return false;
        }
        //[JsMethod(InlineCodeExpression="s in obj", Export=false)]
        //public static bool HasProperty(this object obj, string s)
        //{
        //    return false;
        //}

        [JsMethod(OmitDotOperator = true, OmitParanthesis = true, Export = false, NativeOverloads = true, Name = "instanceof", ExtensionImplementedInInstance = true)]
        public static bool instanceof(this object obj, JsFunction ctor)
        {
            return false;
        }
        /// <summary>
        /// Tests for the existence of a property in an object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [JsMethod(OmitDotOperator = true, OmitParanthesis = true, Export = false, NativeOverloads = true, Name = "in", ExtensionImplementedInInstance = true)]
        public static bool @in(this string propertyName, object obj) { return false; }
        
        [JsMethod(InlineCodeExpression = "s in obj", Export = false)]
        public static bool HasProperty(this object obj, string s)
        {
            return false;
        }

        [JsMethod(Export = false, ExtensionImplementedInInstance=true)]
        public static bool hasOwnProperty(this object obj, string s)
        {
            return false;
        }

        /// <summary>
        /// A C# extension for '===' operator
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        [JsMethod(OmitDotOperator = true, OmitParanthesis = true, Export = false, NativeOverloads = true, Name = "===", ExtensionImplementedInInstance = true)]
        public static bool ExactEquals(this object obj, object obj2)
        {
            return false;
        }
        /// <summary>
        /// A C# extension for '!==' operator
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        [JsMethod(OmitDotOperator = true, OmitParanthesis = true, Export = false, NativeOverloads = true, Name = "!==", ExtensionImplementedInInstance = true)]
        public static bool ExactNotEquals(this object obj, object obj2)
        {
            return false;
        }
    }

    #endregion

    [JsType(JsMode.Prototype, NativeEnumerator = false, NativeArrayEnumerator = true, Export = false)]
    public interface IJsArrayEnumerable<T> : IEnumerable<T>
    {
        [JsProperty(NativeIndexer = true)]
        T this[JsNumber index] { get; }
        [JsProperty(NativeField = true)]
        JsNumber length { get; }

    }

    #region Namespace Documentation
    /// <summary>
    /// Contains all SharpKit compiler attributes used to customize JavaScript output
    /// Contains .NET types for primitive JavaScript types such as JsNumber, JsObject, JsArray, etc...
    /// Contains all JavaScript global functions in a derivable context class - JsContext
    /// </summary>
    /// <example>
    /// This example uses the JsTypeAttribute to instruct SharpKit compiler to convert this class into JavaScript,
    /// it also uses the parseInt global JavaScript function, and the toString() function on the JsNumber type.
    /// <code>
    /// using SharpKit.JavaScript;
    /// 
    /// namespace MyApp.Client
    /// {
    ///     [JsType(JsMode.Global, Filename="MyScript.js")]
    ///     class MyScript : JsContext
    ///     {
    ///         public static void Main()
    ///         {
    ///             var x = parseInt("677");
    ///             var s = x.toString();
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    class NamespaceDoc
    {
    }

    #endregion
}

//namespace SharpKit.JavaScript.Server
//{
//    using System.Reflection;
//    using System.Linq;
//    /// <summary>
//    /// Server side javascript helper, helps binding from C# / aspx files to JavaScript methods
//    /// Method names are cached
//    /// </summary>
//    public class JsBinder
//    {
//        static object MethodsEntrance = new object();
//        static Dictionary<MethodInfo, string> Methods = new Dictionary<MethodInfo, string>();
//        /// <summary>
//        /// Returns javascript code that compiles all jsclr classes (exactly like JsCompiler.Compile() or JsRuntime.Start())
//        /// </summary>
//        /// <returns></returns>
//        public string Compile()
//        {
//            return "Compile();";
//        }
//        public string ActionOf(Action action)
//        {
//            return MethodOf(action.Method);
//        }
//        public string jReady(Action action)
//        {
//            return String.Format("$({0});", ActionOf(action));
//        }
//        public string InvokeAction(Action action)
//        {
//            return String.Format("{0}();", ActionOf(action));
//        }
//        public string HandleEvent(Action action)
//        {
//            return InvokeAction(action);
//        }
//        T GetAttribute<T>(MemberInfo mi) where T : Attribute
//        {
//            var list = mi.GetCustomAttributes(typeof(T), true);
//            if (list.Length == 0)
//                return default(T);
//            return (T)list[0];
//        }
//        public string MethodOf(MethodInfo me)
//        {
//            string name;
//            if (!Methods.TryGetValue(me, out name))
//            {
//                lock (MethodsEntrance)
//                {
//                    if (!Methods.TryGetValue(me, out name))
//                    {
//                        name = MethodOfNoCache(me);
//                        Methods[me] = name;
//                    }
//                }
//            }
//            return name;
//        }
//        string MethodOfNoCache(MethodInfo me)
//        {
//            var att = GetAttribute<JsMethodAttribute>(me);
//            var meName = me.Name;
//            var ceName = me.DeclaringType.FullName;
//            var isGlobal = false;
//            if (att != null)
//            {
//                if (att.Name != null)
//                    meName = att.Name;
//                if (att.Global)
//                    isGlobal = true;
//                if (att.GlobalCode)
//                    throw new Exception("Cannot get method name of global code");
//            }
//            if (isGlobal)
//            {
//                return meName;
//            }
//            else
//            {
//                var att2 = GetAttribute<JsTypeAttribute>(me.DeclaringType);
//                if (att2 != null)
//                {
//                    if (att2.GlobalObject || att2.Mode == JsMode.Global)
//                        isGlobal = true;
//                    if (att2.Name != null)
//                        ceName = att2.Name;
//                }
//                if (isGlobal || String.IsNullOrEmpty(ceName))
//                    return meName;

//                if (att2 == null || att2.Name == null)
//                {
//                    var atts = me.DeclaringType.Assembly.GetCustomAttributes(typeof(JsNamespaceAttribute), true).OfType<JsNamespaceAttribute>().OrderByDescending(t => t.Namespace.Length).ToList();
//                    foreach (var att3 in atts)
//                    {
//                        if (ceName.StartsWith(att3.Namespace))
//                        {
//                            if (String.IsNullOrEmpty(att3.JsNamespace))
//                                ceName = ceName.Replace(att3.Namespace + ".", att3.JsNamespace);
//                            else
//                                ceName = ceName.Replace(att3.Namespace, att3.JsNamespace);
//                            break;
//                        }
//                    }
//                }
//                if (String.IsNullOrEmpty(ceName))
//                    return meName;
//                return String.Format("{0}.{1}", ceName, meName);
//            }
//        }
//    }



//}