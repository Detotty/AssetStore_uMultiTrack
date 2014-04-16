/**
* Copyright (C) 2005-2014 by Rivello Multimedia Consulting (RMC).                    
* code [at] RivelloMultimediaConsulting [dot] com                                                  
*                                                                      
* Permission is hereby granted, free of charge, to any person obtaining
* a copy of this software and associated documentation files (the      
* "Software"), to deal in the Software without restriction, including  
* without limitation the rights to use, copy, modify, merge, publish,  
* distribute, sublicense, and#or sell copies of the Software, and to   
* permit persons to whom the Software is furnished to do so, subject to
* the following conditions:                                            
*                                                                      
* The above copyright notice and this permission notice shall be       
* included in all copies or substantial portions of the Software.      
*                                                                      
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,      
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF   
* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
* IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR    
* OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
* ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
* OTHER DEALINGS IN THE SOFTWARE.                                      
*/
// Marks the right margin of code *******************************************************************


//--------------------------------------
//  Imports
//--------------------------------------
using UnityEngine;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.multi_track
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------
	
	
	//--------------------------------------
	//  Class
	//--------------------------------------
	public class Track 
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		
		// PUBLIC
		/// <summary>
		/// The audio clip.
		/// </summary>
		private AudioClip _audioClip;
		public AudioClip audioClip
		{
			get {
				return _audioClip;
			}

		}

		
		/// <summary>
		/// The isPlaying status
		/// </summary>
		internal bool _isPlaying_boolean;
		public bool isPlaying
		{
			get 
			{
				return _isPlaying_boolean;

			}
		}


		/// <summary>
		/// The _minimum intensity_float.
		/// </summary>
		private float _minimumIntensity_float;
		public float minimumIntensity
		{
			get {
				return _minimumIntensity_float;
			}
			
		}

		
		/// <summary>
		/// The _maximum intensity_float.
		/// </summary>
		private float _maximumIntensity_float;
		public float maximumIntensity
		{
			get {
				return _maximumIntensity_float;
			}
			
		}

		/// <summary>
		/// DETERMINE IF VALID INTENSITY
		/// </summary>
		public bool isValidIntensity (float aIntensity_float)
		{
			bool isValid_boolean = false;
			if (aIntensity_float >= _minimumIntensity_float &&
			    aIntensity_float <= _maximumIntensity_float) {
				isValid_boolean = true;
			}

			return isValid_boolean;
		}
		
		
		// PUBLIC STATIC
		
		// PRIVATE
		// PRIVATE STATIC
		
		
		//--------------------------------------
		//  Methods
		//--------------------------------------	
		// PUBLIC
		
		///<summary>
		///	 Constructor
		///</summary>
		public Track (AudioClip aAudioClip, float aMinimumIntensity_float, float aMaximumIntensity_float)
		{
			_audioClip = aAudioClip;
			//
			_minimumIntensity_float = aMinimumIntensity_float;
			_maximumIntensity_float = aMaximumIntensity_float;
			//
			_isPlaying_boolean = false;

		}
		
		/// <summary>
		/// Deconstructor
		/// </summary>
		~Track ( )
		{
			
		}
		
		
		// PUBLIC
		
		
		// PUBLIC STATIC
		/// <summary>
		/// Froms the audio asset.
		/// </summary>
		/// <returns>The audio asset.</returns>
		/// <param name="aAudioClipName_string">A audio clip name_string.</param>
		public static Track fromAudioAsset (string aAudioClipName_string, float aMinimumIntensity_float, float aMaximumIntensity_float)
		{

			//
			AudioClip audioClip = Resources.Load (aAudioClipName_string) as AudioClip;
			if (audioClip == null) {

				throw new UnityException ("Audio Not Found For Path '"+ aAudioClipName_string+"'");
			}
			return new Track (audioClip, aMinimumIntensity_float, aMaximumIntensity_float);

		}
		
		// PRIVATE
		
		// PRIVATE STATIC
		
		// PRIVATE COROUTINE
		
		// PRIVATE INVOKE
		
		//--------------------------------------
		//  Events
		//--------------------------------------
		
		
		
	}
}