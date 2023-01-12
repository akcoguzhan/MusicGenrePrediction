import sys
import subprocess
import os
from pydub import AudioSegment

def convert_to_wav(filePath, fileName):
    outputdirectory = os.path.join(os.getenv('APPDATA'), 'Capstone');

    if not os.path.exists(outputdirectory):
        os.mkdir(outputdirectory)

    if os.path.exists(os.path.join(outputdirectory, 'original.wav')):
        os.remove(os.path.join(outputdirectory, 'original.wav'))
    subprocess.call(['ffmpeg', '-y', '-i', str(os.path.join(filePath, fileName)),
                        str(os.path.join(outputdirectory, 'original.wav'))],
                    stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)

    outputfile = os.path.join(outputdirectory, 'original.wav')
    outputfile = outputfile.replace('\r', '')
    outputfile = outputfile.replace('\n', '')
    print(outputfile)

def crop(filePath, fileName, start, duration):
    start = int(start)
    duration = int(duration)
    outputdirectory = os.path.join(os.getenv('APPDATA'), 'Capstone');

    sound = AudioSegment.from_file(str(os.path.join(filePath, fileName)))
    if duration == 0:
        new_sound = sound[start*1000:-1]
        new_file_path = os.path.join(outputdirectory, 'cropped.wav')
        new_sound.export(new_file_path, format='wav')
        new_file_path = new_file_path.replace('\r', '')
        new_file_path = new_file_path.replace('\n', '')
        print(new_file_path)
    else:
        new_sound = sound[start*1000:((start*1000) + (duration*1000))]
        new_file_path = os.path.join(outputdirectory, 'cropped.wav')
        new_sound.export(new_file_path, format='wav')
        new_file_path = new_file_path.replace('\r', '')
        new_file_path = new_file_path.replace('\n', '')
        print(new_file_path)
    

if __name__ == '__main__':
    args = sys.argv
    globals()[args[1]](*args[2:])
