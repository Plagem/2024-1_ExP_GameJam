using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using AudioSource = UnityEngine.AudioSource;

public class SoundManager
{
     public const string NAME = "@Sound";

        public enum SoundType
        {
            BGM,
            Effect,
            MAX
        }

        static private SoundManager instance;

        static public SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                    instance.init();
                }

                return instance;
            }
        }

        /// <summary>
        /// BGM와 효과음은 오디오 소스 분리
        /// </summary>
        private AudioSource _bgmSource;

        private LinkedList<AudioSource> _effectSources;
        private LinkedList<string> _playingEffects;

        /// <summary>
        /// 소리가 미리 저장되어 있는 딕셔너리
        /// </summary>
        private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        private AudioMixer mixer;
        private AudioMixerGroup bgmGroup;
        private AudioMixerGroup effectGroup;

        public SoundManager()
        {
            _effectSources = new LinkedList<AudioSource>();
            _playingEffects = new LinkedList<string>();
        }

        public void init()
        {
            GameObject root = GameObject.Find(NAME);
            if (root == null)
            {
                root = new GameObject { name = NAME };
                Object.DontDestroyOnLoad(root);

                string[] soundNames = System.Enum.GetNames(typeof(SoundType)); // "Bgm", "Effect"

                GameObject bgm_go = new GameObject { name = soundNames[(int)SoundType.BGM] };
                _bgmSource = bgm_go.AddComponent<AudioSource>();
                bgm_go.transform.parent = root.transform;

                GameObject effect_go = new GameObject { name = soundNames[(int)SoundType.Effect] };
                effect_go.transform.parent = root.transform;
                this.effect_go = effect_go;
                AddEffectSource();
                mixer = Resources.Load<AudioMixer>("MainMixer");
                bgmGroup = mixer.FindMatchingGroups("BGM")[0];
                effectGroup = mixer.FindMatchingGroups("Effect")[0];

                _tickingSource = effect_go.AddComponent<AudioSource>();

                _tickingSource.loop = true;
                _bgmSource.loop = true; // bgm 재생기는 무한 반복 재생
            }
        }
        /// <summary>
        /// 띡똑
        /// </summary>
        private AudioSource _tickingSource;
        
        private GameObject effect_go;

        public void PlayTick()
        {
            AudioClip clip = GetOrAddAudioClip("7. tick_tock");
            AudioSource audioSource = _tickingSource;
            audioSource.pitch = 1.0f;
            audioSource.volume = 1.0f;
            audioSource.outputAudioMixerGroup = effectGroup;
            audioSource.clip = clip;
            audioSource.Play(); 
        }

        public void StopTick
        (){
            _tickingSource.Stop();
        }

        public void Play(string path, SoundType type = SoundType.Effect, float pitch = 1.0f, float volume = 1.0f)
        {
            AudioClip audioClip = GetOrAddAudioClip(path, type);
            Play(audioClip, type, pitch, volume);
        }

        public void Play(AudioClip audioClip, SoundType type = SoundType.Effect, float pitch = 1.0f,
            float volume = 1.0f)
        {
            if (audioClip == null)
                return;
            // Debug.Log(audioClip);
            if (type == SoundType.BGM) // BGM 배경음악 재생
            {
                AudioSource audioSource = _bgmSource;
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.volume = volume;
                audioSource.outputAudioMixerGroup = bgmGroup;
                audioSource.Play();
            }
            else // Effect 효과음 재생
            {
                AudioSource audioSource = GetEffectSource();
                audioSource.pitch = pitch;
                audioSource.volume = volume;
                audioSource.outputAudioMixerGroup = effectGroup;
                audioSource.PlayOneShot(audioClip);
                _playingEffects.AddLast(audioClip.name);
            }
        }

        public AudioSource GetEffectSource()
        {
            foreach (var source in _effectSources)
            {
                if (source.isPlaying == false)
                    return source;
                
            }
            

            return AddEffectSource();
        }

        private AudioSource AddEffectSource()
        {
            var source = effect_go.AddComponent<AudioSource>();
            _effectSources.AddLast(source);
            return source;
        }

        /// <summary>
        /// 오디오 클립을 받아오는 함수
        /// effect의 경우 필요할때마다 받아올 시 오버헤드가 있으므로 Dictionary에 저장한다.
        /// </summary>
        /// <param name="path">BGM경로</param>
        /// <param name="type">BGM종류, 지정없을시 Effect</param>
        /// <returns>해당 AudioClup (없을시 null)</returns>
        public AudioClip GetOrAddAudioClip(string patha, SoundType type = SoundType.Effect)
        {
            string path = patha;
            if (path.Contains("Sound/") == false)
                path = $"Sound/{(SoundType.BGM == type ? "bgm" : "Effect")}/{path}"; // Sound 폴더 안에 저장될 수 있도록

            AudioClip audioClip;

            if (type == SoundType.BGM) // BGM 배경음악 클립 붙이기
            {
                audioClip = Resources.Load<AudioClip>(path);
            }
            else // Effect 효과음 클립 붙이기
            {
                if (_audioClips.TryGetValue(path, out audioClip) == false)
                {
                    audioClip = Resources.Load<AudioClip>(path);
                    _audioClips.Add(path, audioClip);
                }
            }

            if (audioClip == null)
                Debug.Log($"AudioClip Missing ! {path}");

            return audioClip;
        }

        /// <summary>
        /// BGM의 볼륨 조절
        /// </summary>
        /// <param name="val">0.0001 ~ 1의 값</param>
        public void ChangeVolumeBGM(float val)
        {
            mixer.SetFloat("BGM", Mathf.Log10(val) * 20);
        }

        /// <summary>
        /// Effect의 볼륨 조절
        /// </summary>
        /// <param name="val">0.0001 ~ 1의 값</param>
        public void ChangeVolumeEffect(float val)
        {
            mixer.SetFloat("Effect", Mathf.Log10(val) * 20);
        }
        
        public void PauseBGM()
        {
            _bgmSource.Pause();
        }

        public void UnPauseBGM()
        {
            _bgmSource.UnPause();
        }

        public void PauseEffect()
        {
            foreach (var source in _effectSources)
            {
                source.Pause();
            }
            _tickingSource.Pause();
        }

        public void UnPauseEffect()
        {
            foreach (var source in _effectSources)
            {
                source.UnPause();
            }
            _tickingSource.UnPause();
        }

        /// <summary>
        /// bgm 소스에 직접 접근함.
        /// </summary>
        /// <remarks>사용 할 일 없음!</remarks>
        /// <param name="val">0 ~ 1 의 값</param>
        public void ChangeSrcVolumeBGM(float val)
        {
            _bgmSource.volume = val;
        }

        public bool optVibration;

        public void DoVibration()
        {

            if (optVibration)
            {
#if UNITY_ANDROID
            // Vibration.Vibrate(150);

#endif
#if UNITY_EDITOR
                Debug.Log("Vibration!");
#endif
            }

        }

        /// <summary>
        /// effect 리스트를 돌며 볼륨 변경
        /// 만약 재생중이 아닌 소스가 있을경우 삭제
        /// </summary>
        /// <remarks>사용할 일 없음!</remarks>
        /// <param name="val">0~1의 값</param>
        public void ChangeSrcVolumeEffect(float val)
        {
            var nodeSrc = _effectSources.First;
            if (nodeSrc == null)
                return;
            do
            {
                var src = nodeSrc.Value;
                nodeSrc = nodeSrc.Next;
                if (src.isPlaying)
                {
                    src.volume = val;
                }
                // else
                // {
                //     _effectSources.Remove(nodeSrc);
                // }
            } while (nodeSrc != null);
        }
        
        public void Clear()
        {
            _bgmSource.clip = null;
            _bgmSource.Stop();

            // 재생기 전부 재생 스탑, 음반 빼기
            foreach (AudioSource audioSource in _effectSources)
            {
                audioSource.clip = null;
                audioSource.Stop();
            }

            // 효과음 Dictionary 비우기
            _audioClips.Clear();
            // 재생중 효과음 이름 지우기
            _playingEffects.Clear();
        }

    }
