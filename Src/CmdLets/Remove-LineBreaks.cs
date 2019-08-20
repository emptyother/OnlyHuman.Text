using System.Management.Automation;

namespace OnlyHuman.Text
{
	/// <summary>
	/// Remove linebreaks.
	/// </summary>
	[Cmdlet(VerbsCommon.Remove, "LineBreaks")]
	[OutputType(typeof(string))]
	public class Remove_LineBreaks : PSCmdlet
	{
		[Parameter(
			Position = 0,
			ValueFromPipeline = true
		)]
		public string[] InputText { get; set; }

		protected override void BeginProcessing()
		{
		}

		protected override void ProcessRecord()
		{
			foreach (var line in InputText)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					WriteObject(line);
				}
			}
		}

		protected override void EndProcessing()
		{
		}
	}
}
