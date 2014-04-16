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
using System.Collections.Generic;
using com.rmc.projects.multi_track;
using System.Collections;


//--------------------------------------
//  Namespace
//--------------------------------------
namespace com.rmc.projects.multi_track
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------
	public enum TrackUpdateType
	{
		DEPENDS_ON_INTENSITY,
		FORCE_STOP,
		FORCE_PLAY

	}
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------
	
	
	//--------------------------------------
	//  Class
	//--------------------------------------
	public class MultiTrack 
	{
		
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER

		/// <summary>
		/// The _tracks_list.
		/// </summary>
		private List<Track> _tracks_list;
		public List<Track> tracks
		{
			get {
				return _tracks_list;
			}
		}



		/// <summary>
		/// The _intensity_float.
		/// </summary>
		private float _intensity_float;
		public float intensity
		{
			set {
				//TODO: DYNAMIZE MIN/MAX USED HERE
				if (_intensity_float != value) {
					_intensity_float = Mathf.Clamp (value, 0, 100);

					//
					foreach (Track track in _tracks_list) {
						_updateStatusOfTrack (track, TrackUpdateType.DEPENDS_ON_INTENSITY);
					}
				}
			}
			get {

				return _intensity_float;
			}


		}

		
		/// <summary>
		/// The _track cross fade duration_float.
		/// 
		/// NOTE: Used for fade in and fade out, automatically
		/// TODO: test '0' as a value. May break
		/// 
		/// </summary>
		private float _trackCrossFadeDuration_float = 1; //1 second by default
		public float trackCrossFadeDuration
		{
			set {
				_trackCrossFadeDuration_float = value;
			}
			get {
				
				return _trackCrossFadeDuration_float;
			}
			
			
		}




		/// <summary>
		/// The isPlaying status
		/// </summary>
		private bool _isPlaying_boolean;
		public bool isPlaying
		{
			set {
				//TODO: DYNAMIZE MIN/MAX USED HERE
				if (_isPlaying_boolean != value) {
					_isPlaying_boolean = value;

					if (_isPlaying_boolean) {

						//
						foreach (Track track in _tracks_list) {
							_updateStatusOfTrack (track, TrackUpdateType.DEPENDS_ON_INTENSITY);
						}
					} else {

						//
						foreach (Track track in _tracks_list) {
							_updateStatusOfTrack (track, TrackUpdateType.FORCE_STOP);
						}
					}

				}
			}
			get {
				
				return _isPlaying_boolean;
			}
			
			
		}


		
		/// <summary>
		/// Play 
		/// </summary>
		public void play ()
		{
			foreach (Track track in _tracks_list) {
				
				_updateStatusOfTrack (track, TrackUpdateType.DEPENDS_ON_INTENSITY);
				
			}
		}
		
		
		/// <summary>
		/// Play 
		/// </summary>
		public void stop ()
		{
			foreach (Track track in _tracks_list) {
				
				_updateStatusOfTrack (track, TrackUpdateType.FORCE_STOP);
				
			}
		}


		
		// PUBLIC
		
		// PUBLIC STATIC
		
		// PRIVATE
		/// <summary>
		/// The _audio parent_gameobject.
		/// </summary>
		private GameObject _audioParent_gameobject;

		/// <summary>
		/// The _audio parent component.
		/// </summary>
		private AudioParentComponent _audioParentComponent;


		/// <summary>
		/// The _synchonizer loop duration_float.
		/// 
		/// NOTE: A tick duration will match the music asset's duration too.
		/// NOTE: This system helps tracks to 'stay in sync' with each other
		/// 
		/// </summary>
		private float _synchonizerTickDuration_float = 1; //start with 10 seconds, but this is corrected below soon.

		// PRIVATE STATIC
		
		
		//--------------------------------------
		//  Methods
		//--------------------------------------	
		// PUBLIC
		
		///<summary>
		///	 Constructor
		///</summary>
		public MultiTrack (GameObject aAudioParent_gameobject)
		{

			//
			_audioParent_gameobject = aAudioParent_gameobject;
			if (_audioParent_gameobject.GetComponent<AudioParentComponent>() == null){
				_audioParent_gameobject.AddComponent<AudioParentComponent>();
			}
			_audioParentComponent = _audioParent_gameobject.GetComponent<AudioParentComponent>();
			_audioParentComponent.StartCoroutine (onSyncronizerTick_Coroutine());

			//
			_intensity_float = 100;

			///
			clearTracks();
		}


		
		/// <summary>
		/// Deconstructor
		/// </summary>
		~MultiTrack ( )
		{
			
		}
		
		
		///<summary>
		///	Called once per frame
		///</summary>
		void Update () 
		{
			
			
		}
		
		// PUBLIC
		/// <summary>
		/// Clears the tracks.
		/// </summary>
		public void clearTracks()
		{
			_tracks_list = new List<Track>();


		}

		/// <summary>
		/// Adds the track.
		/// </summary>
		/// <param name="aTrack">A track.</param>
		public void addTrack (Track aTrack)
		{
			_tracks_list.Add (aTrack);
			if (_tracks_list.Count == 1) {
				//ARBITRATRILY SET tick duration to match the very first track added
				_synchonizerTickDuration_float = aTrack.audioClip.length;
			}
			//Debug.Log ("added: " + _tracks_list);
		}

		
		// PUBLIC STATIC
		
		// PRIVATE
		/// <summary>
		/// Initializes a new instance of the <see cref="com.rmc.audio.multi_track.MultiTrack"/> class.
		/// </summary>
		private void _updateStatusOfTrack (Track aTrack, TrackUpdateType aTrackUpdateType)
		{
			AudioSource audioSource = _getBestAudioSourceForTrack (aTrack);

			switch (aTrackUpdateType)
			{
			case TrackUpdateType.FORCE_PLAY:
				if (_isPlaying_boolean) {
					if (audioSource.isPlaying == false) {
						audioSource.clip = aTrack.audioClip;
						audioSource.loop = true;
						aTrack._isPlaying_boolean = true;

						//SEEK TO THE MOMENT TO MATCH THE FIRST TRACK
						AudioSource first_audioSource = _getBestAudioSourceForTrack (_tracks_list[0]);
						if (audioSource == first_audioSource) {
							audioSource.time = 0;
							//Debug.Log ("First");
						} else {
							audioSource.time = first_audioSource.time;
						}

						//FADE VOLUME
						_doFadeAudioSourceFromStopToPlay(audioSource);
					}
				}
				break;
			case TrackUpdateType.FORCE_STOP:
				if (audioSource.isPlaying == true) {
					aTrack._isPlaying_boolean = false;
					//FADE VOLUME
					_doFadeAudioSourceFromPlayToStop(audioSource);
				}
				break;
			case TrackUpdateType.DEPENDS_ON_INTENSITY:
				if (aTrack.isValidIntensity (_intensity_float)) {
					_updateStatusOfTrack (aTrack, TrackUpdateType.FORCE_PLAY);
				} else {
					_updateStatusOfTrack (aTrack, TrackUpdateType.FORCE_STOP);
				}
				
				break;
			}
			//Debug.Log ("updating : " + aTrack.audioClip);

		}

		/// <summary>
		/// _gets the audio source
		/// </summary>
		private AudioSource _getBestAudioSourceForTrack (Track aTrack)
		{
			/*
			 * 
			 * TODO: THIS CAN BE GREATLY OPTIMIZED
			 * 
			 * 
			 * 
			 * 
			 * 
			 **/

			//TODO: CREATE ONLY WHAT WE NEED
			//TEMP: CREATE MORE THAN ENOUGH
			if (_audioParent_gameobject.GetComponent<AudioSource>() == null) {
				_audioParent_gameobject.AddComponent<AudioSource>();
				_audioParent_gameobject.AddComponent<AudioSource>();
				_audioParent_gameobject.AddComponent<AudioSource>();
				_audioParent_gameobject.AddComponent<AudioSource>();
				_audioParent_gameobject.AddComponent<AudioSource>();
				_audioParent_gameobject.AddComponent<AudioSource>();
				_audioParent_gameobject.AddComponent<AudioSource>();
				_audioParent_gameobject.AddComponent<AudioSource>();
			}

			//
			AudioSource chosen_audiosource = null;
			AudioSource[] allAvailable_audiosource = _audioParent_gameobject.GetComponents<AudioSource>();


			//1
			//TODO: OPTIMIZE
			//TEMP: ALREADY A MATCH?
			if (chosen_audiosource == null) {
				foreach (AudioSource audioSource in allAvailable_audiosource) {
					if (audioSource.clip == aTrack.audioClip) {
						chosen_audiosource = audioSource;
						break;
					}
				}
			}

			//2
			//TODO: OPTIMIZE
			//TEMP: FIRST ONE NOT PLAYING WILL BE OUR SELECTION
			if (chosen_audiosource == null) {
				foreach (AudioSource audioSource in allAvailable_audiosource) {
					if (audioSource.isPlaying == false) {
						chosen_audiosource = audioSource;
						break;
					}
				}
			}

			//3
			//TEMP: ERROR
			if (chosen_audiosource == null) {
				throw new MissingReferenceException ("No AudioSource Found for '"+ aTrack.audioClip+"'");
			}


			return chosen_audiosource;
		}


		/// <summary>
		/// _dos the fade audio source from stop to play.
		/// </summary>
		/// <param name="aAudioSource">A audio source.</param>
		private void _doFadeAudioSourceFromStopToPlay (AudioSource aAudioSource)
		{

			//FADE SOUND FROM 0 TO 100% OVER X SECONDS
			aAudioSource.volume = 0;
			Hashtable audioTo_hashtable 							= new Hashtable();
			audioTo_hashtable.Add(iT.AudioTo.volume,  				1);
			audioTo_hashtable.Add(iT.AudioTo.time,  				_trackCrossFadeDuration_float);
			audioTo_hashtable.Add(iT.AudioTo.pitch,  				1);
			audioTo_hashtable.Add(iT.AudioTo.audiosource, 			aAudioSource);
			iTween.AudioTo (_audioParent_gameobject, 				audioTo_hashtable);

			aAudioSource.Play();
			
		}

		/// <summary>
		/// _dos the fade audio source from play to stop.
		/// </summary>
		/// <param name="aAudioSource">A audio source.</param>
		private void _doFadeAudioSourceFromPlayToStop (AudioSource aAudioSource)
		{
			
			//FADE SOUND FROM 0 TO 100% OVER X SECONDS
			//aAudioSource.volume = 1;
			Hashtable audioTo_hashtable 							= new Hashtable();
			audioTo_hashtable.Add(iT.AudioTo.volume,  				0);
			audioTo_hashtable.Add(iT.AudioTo.time,  				_trackCrossFadeDuration_float);
			audioTo_hashtable.Add(iT.AudioTo.pitch,  				1);
			audioTo_hashtable.Add(iT.AudioTo.audiosource, 			aAudioSource);
			iTween.AudioTo (_audioParent_gameobject, 				audioTo_hashtable);

			//CAPTURE WHEN ITS COMPLETED
			_audioParentComponent.StartCoroutine (onFadeToStopComplete_Coroutine (aAudioSource, _trackCrossFadeDuration_float));
			
		}
		
		// PRIVATE STATIC
		
		// PRIVATE COROUTINE
		
		// PRIVATE INVOKE
		
		//--------------------------------------
		//  Events
		//--------------------------------------
		/// <summary>
		/// Ons the syncronizer tick_ coroutine.
		/// 
		/// 
		/// NOTE: If I don't find a need for htis method...
		/// 		1. REMOVE IT
		/// 
		/// 
		/// </summary>
		/// <returns>The syncronizer tick_ coroutine.</returns>
		IEnumerator onSyncronizerTick_Coroutine()
		{
			//WAIT
			yield return new WaitForSeconds(_synchonizerTickDuration_float);

			//DO STUFF
			//TODO: FIGURE OUT WHAT WE MIGHT WWANT TO DO AT THE 'END' OF THE LOOP DURATION HERE...
			//I HAD THOUGHT THAT ADD/REMOVE TRACKS WOULD ONLY HAPPEN HERE, BUT ITS TO INFREQUENT
			//Debug.Log("onSyncronizerTick_Coroutine: " + _synchonizerTickDuration_float);


			//REPEAT
			_audioParentComponent.StartCoroutine (onSyncronizerTick_Coroutine());
			
		}

		/// <summary>
		/// Ons the fade to stop complete_ coroutine.
		/// </summary>
		/// <returns>The fade to stop complete_ coroutine.</returns>
		/// <param name="">.</param>
		IEnumerator onFadeToStopComplete_Coroutine (AudioSource aAudioSource, float aTrackCrossFadeDuration_float)
		{

			yield return new WaitForSeconds (aTrackCrossFadeDuration_float);
			aAudioSource.Stop();

			yield return null;
		}


		
	}
}