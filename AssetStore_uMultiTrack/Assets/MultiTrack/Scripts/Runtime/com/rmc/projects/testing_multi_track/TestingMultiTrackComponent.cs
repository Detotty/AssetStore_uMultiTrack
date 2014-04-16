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
using com.rmc.projects.multi_track;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.testing_multi_track
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
	public class TestingMultiTrackComponent : MonoBehaviour 
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		
		// PUBLIC
		/// <summary>
		/// The empty audio GameObject
		/// </summary>
		public GameObject emptyAudioGO;

		/// <summary>
		/// The intensity.
		/// </summary>
		[Range (0, 100)]
		public float intensity = 30;

		// PUBLIC STATIC
		
		// PRIVATE
		/// <summary>
		/// The multi track.
		/// </summary>
		private MultiTrack _multiTrack;

		/// <summary>
		/// The _tracks playing_uint.
		/// </summary>
		private uint _tracksPlaying_uint = 0;
		
		// PRIVATE STATIC

		
		//--------------------------------------
		//  Methods
		//--------------------------------------	
		// PUBLIC
		
		///<summary>
		///	 Constructor
		///</summary>
		public TestingMultiTrackComponent ()
		{

		}
		
		/// <summary>
		/// Deconstructor
		/// </summary>
		~TestingMultiTrackComponent ( )
		{
			
			
		}





		///<summary>
		///	Use this for initialization
		///</summary>
		void Start () 
		{


			//CREATE INSTANCE
			_multiTrack = new MultiTrack (emptyAudioGO); 


			//(OPTIONAL) CLEAR ALL CURRENT TRACKS
			_multiTrack.clearTracks();

			//(OPTIONAL) STOP THE SOUNDS AT ANY TIME
			_multiTrack.isPlaying = false;

			
			//----------------------------------------------------------------------------------------------------
			//VOLUME
			//				0//////////[20]//////////[40]//////////[60]//////////[80]/////[90]///////100
			//TRACKS
			//							1			2				3			4			5
			//
			//----------------------------------------------------------------------------------------------------

			//ADD TRACKS BY: PATH, MIN INTENSITY, MAX INTENSITY
			_multiTrack.addTrack ( Track.fromAudioAsset ("Audio/Music/song_1_layer2_v1.ogg", 20, 100));
			_multiTrack.addTrack ( Track.fromAudioAsset ("Audio/Music/song_1_layer3_v1.ogg", 40, 100));
			_multiTrack.addTrack ( Track.fromAudioAsset ("Audio/Music/song_1_layer4_v1.ogg", 60, 100));
			_multiTrack.addTrack ( Track.fromAudioAsset ("Audio/Music/song_1_layer5_v1.ogg", 80, 100));
			_multiTrack.addTrack ( Track.fromAudioAsset ("Audio/Music/song_1_layer1_v1.ogg", 90, 100));
			
			
			//(OPTIONAL) FADE
			_multiTrack.trackCrossFadeDuration = 1f; //e.g. 2 secs


			//NOTE, SET INTENSITY ONLY IN 'UPDATE' BELOW
			//CHANGE INTENSITY ONLY IN 'UPDATE' BELOW
			_multiTrack.intensity = intensity = 0;

			
			//PLAY!
			_multiTrack.isPlaying = true;




		
			
		}









		/// <summary>
		/// Raises the GUI event.
		/// 
		/// ****UPDATE GUI, BUT NOT AUDIO*****
		/// 
		/// </summary>
		void OnGUI() {
			
			//INSTRUCTIONS
			GUI.Label (new Rect (25, 25, 500, 100), "<MultiTrack>\n\n\t\t\t\t\t\t\tDemo of a dynamic song based on gameplay (e.g. Intensity)");

			//OPTIONS
			GUI.Label (new Rect (25, 115, 500, 100), "<Options>");

			//INTENSITY SLIDER
			GUI.Label (new Rect (25, 140, 500, 100), "Intensity: ");
			intensity = GUI.HorizontalSlider(new Rect(130, 148, 300, 20), intensity, 0.0F, 100.0F);
			
			//STOP/PLAY BUTTON
			if (GUI.Button (new Rect (130,170,80,20), "Stop/Play")) {
				_multiTrack.isPlaying = !_multiTrack.isPlaying;
			}

			//OUTPUT
			GUI.Label (new Rect (25, 230, 500, 100), "<Output>");
			GUI.Label (new Rect (25, 250, 500, 100), "Tracks In Use:\t\t" + _tracksPlaying_uint);
			GUI.Label (new Rect (25, 270, 500, 100), "IsPlaying:\t\t\t\t" + _multiTrack.isPlaying);
			GUI.Label (new Rect (25, 290, 500, 100), "Intensity:\t\t\t\t" + intensity.ToString ("0.00"));


			return ;
		}


		
		///<summary>
		///	Called once per frame
		/// 
		/// ****UPDATE ACTUAL AUDIO*****
		/// 
		///</summary>
		void Update () 
		{

			//SET INTENSITY
			_multiTrack.intensity = intensity;


			//COUNT THE TRACKS (FOR DEMO'S DISPLAY ONLY)
			_tracksPlaying_uint = 0;
			foreach (Track track in _multiTrack.tracks) {
				if (track.isPlaying == true) {
					_tracksPlaying_uint++;
				}
			}

		}
		
		// PUBLIC

		
		// PUBLIC STATIC
		
		// PRIVATE
		
		// PRIVATE STATIC
		
		// PRIVATE COROUTINE
		
		// PRIVATE INVOKE
		
		//--------------------------------------
		//  Events
		//--------------------------------------
		
		
		
	}
}