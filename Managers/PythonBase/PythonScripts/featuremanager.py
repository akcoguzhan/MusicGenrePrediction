import json
import sys
import librosa
from librosa import display 
import os
import numpy as np
import matplotlib.pyplot as plt
import matplotlib

all_features = {
        "counter": "",
        "filename": "",
        "length": "",
        "chroma_stft_mean": "",
        "rms_mean": "",
        "spectral_centroid_mean": "",
        "spectral_bandwidth_mean": "",
        "rolloff_mean": "",
        "zero_crossing_rate_mean": "",
        "harmony_mean": "",
        "perceptr_mean": "",
        "tempo": "",
        "mfcc1_mean": "",
        "mfcc2_mean": "",
        "mfcc3_mean": "",
        "mfcc4_mean": "",
        "mfcc5_mean": "",
        "mfcc6_mean": "",
        "mfcc7_mean": "",
        "mfcc8_mean": "",
        "mfcc9_mean": "",
        "mfcc10_mean": "",
        "mfcc11_mean": "",
        "mfcc12_mean": "",
        "mfcc13_mean": "",
        "mfcc14_mean": "",
        "mfcc15_mean": "",
        "mfcc16_mean": "",
        "mfcc17_mean": "",
        "mfcc18_mean": "",
        "mfcc19_mean": "",
        "mfcc20_mean": "",
        "label": "",
}

def savePlot(title, feature, sr, saveplot, counter):
    if saveplot == 'True':
        outputdirectory = os.path.join(os.getenv('APPDATA'), 'Capstone', 'Figures');
        if not os.path.exists(os.path.join(outputdirectory, counter)):
            os.mkdir(os.path.join(outputdirectory, counter))
        matplotlib.pyplot.close()
        plt.close()
        plt.cla()
        plt.clf()
        plt.figure(figsize = (16, 6))
        librosa.display.waveshow(y = feature, sr = sr, color ="#A300F9");
        plt.title(counter + "_" + title, fontsize = 23)
        plt.savefig(os.path.join(outputdirectory, counter, str(title) + ".png"), bbox_inches='tight')

def extract_features(track_path: str, track_name: str, savefigures, counter):
    """
    Verilen ses dosyasından öznitelikleri çıkarır
    :param track_path: ses dosyasının dizini
    :param track_name: ses dosyasının adı
    :return: öznitelikler
    """
    feature_list = []
    outputdirectory = os.path.join(os.getenv('APPDATA'), 'Capstone');
    
    y, sr = librosa.load(os.path.join(track_path, track_name))
    n_fft = 2048
    hop_length = 512

    all_features["counter"] = str(counter)

    #region remove silence or unwanted sections
    audio_file, _ = librosa.effects.trim(y)
    #endregion

    #region add track_name as feature
    all_features["filename"] = str(track_name)
    #endregion

    # region Get the length of the audio signal in samples
    track_length = np.shape(audio_file)[0]
    all_features["length"] = str(track_length)
    #endregion

    #region extract chroma_stft feature
    feature_chroma_stft = librosa.feature.chroma_stft(y=audio_file, sr=sr, hop_length=hop_length, n_fft=n_fft)
    all_features["chroma_stft_mean"] = str(feature_chroma_stft.mean())
    savePlot("CHROMA STFT", feature_chroma_stft, sr, savefigures, counter)
    #endregion

    #region extract rms feature
    feature_rms = librosa.feature.rms(y=audio_file, hop_length=hop_length)
    all_features["rms_mean"] = str(feature_rms.mean())
    savePlot("RMS", feature_chroma_stft, sr, savefigures, counter)
    #endregion

    #region extract spectral_centroid feature
    feature_spectral_centroid = librosa.feature.spectral_centroid(y=audio_file, sr=sr, hop_length=hop_length, n_fft=n_fft)
    all_features["spectral_centroid_mean"] = str(feature_spectral_centroid.mean())
    savePlot("Spectral Centroid", feature_spectral_centroid, sr, savefigures, counter)
    #endregion

    #region extract spectral_bandwidth feature
    feature_spectral_bandwidth = librosa.feature.spectral_bandwidth(y=audio_file, sr=sr, hop_length=hop_length, n_fft=n_fft)
    all_features["spectral_bandwidth_mean"] = str(feature_spectral_bandwidth.mean())
    savePlot("Spectral Bandwidth", feature_spectral_bandwidth, sr, savefigures, counter)
    #endregion

    #region extract spectral_rolloff feature
    feature_spectral_rolloff = librosa.feature.spectral_rolloff(y=audio_file, sr=sr, hop_length=hop_length, n_fft=n_fft)
    all_features["rolloff_mean"] = str(feature_spectral_rolloff.mean())
    savePlot("Spectral Rolloff", feature_spectral_rolloff, sr, savefigures, counter)
    #endregion

    #region extract zero_crossing_rate feature
    feature_zero_crossing_rate = librosa.feature.zero_crossing_rate(y=audio_file, hop_length=hop_length)
    all_features["zero_crossing_rate_mean"] = str(feature_zero_crossing_rate.mean())
    savePlot("Zero Crossing Rate", feature_zero_crossing_rate, sr, savefigures, counter)
    #endregion

    #region extract harmony and perceptr
    feature_harmony, feature_perceptr = librosa.effects.hpss(y=audio_file)
    all_features["harmony_mean"] = str(feature_harmony.mean())
    all_features["perceptr_mean"] = str(feature_perceptr.mean())
    #endregion

    #region extract tempo
    tempo, _ = librosa.beat.beat_track(y=audio_file, sr=sr)
    all_features["tempo"] = str(tempo)
    #endregion

    #region extract mffcs
    mfccs = librosa.feature.mfcc(y=audio_file, sr=sr, n_mfcc=20)
    for i in range(0, len(mfccs)):
        savePlot("MFCC_" + str(i+1), mfccs[i], sr, savefigures, counter)
        mfcc = mfccs[i].mean()
        all_features["mfcc" + str(i+1) + "_mean"] = str(mfcc.mean())
    #endregion

    #region add label
    label = str(all_features["filename"]).split(sep=".")[0]
    all_features["label"] = str(label)
    #endregion

    feature_list.append(all_features)

    print(json.dumps(feature_list, indent = 4))

if __name__ == '__main__':
    args = sys.argv
    globals()[args[1]](*args[2:])