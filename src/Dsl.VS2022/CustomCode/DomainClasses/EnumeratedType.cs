namespace ConfigurationSectionDesigner
{
    public partial class EnumeratedType
    {
          #region Convenience Properties

        /// <summary>
        /// Gets a full documentation text based on the user-given <see cref="Documentation"/>.
        /// </summary>
        /// <remarks>
        /// If no user-given documentation is present, a "good enough" default description is returned.
        /// </remarks>
        public string DocumentationText
        {
            get
            {
                return (string.IsNullOrEmpty(this.Documentation) ? string.Format("{0}.", this.Name) : this.Documentation);
            }
        }

        #endregion
    }
}