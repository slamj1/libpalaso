﻿
using System.Collections.Generic;

namespace SIL.Archiving.IMDI.Schema
{
	/// <summary>Interface to simplify access to session files (media and written)</summary>
	public interface IIMDISessionFile
	{
		/// <summary></summary>
		ResourceLinkType ResourceLink { get; set; }

		/// <summary></summary>
		VocabularyType Format { get; set; }

		/// <summary></summary>
		VocabularyType Type { get; set; }

		/// <summary></summary>
		string Size { get; set; }

		/// <summary></summary>
		List<DescriptionType> Description { get; set; }

		/// <summary></summary>
		AccessType Access { get; set; }
	}
}
