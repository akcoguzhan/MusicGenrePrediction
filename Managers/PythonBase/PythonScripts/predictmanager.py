import json
import os
import sys
import pandas as pd
import numpy as np
from keras.models import load_model
import pickle
import matplotlib.pyplot as plt
import matplotlib.cm as cm
from matplotlib.colors import Normalize

modelWeightPath = os.path.join(os.path.dirname(sys.argv[0]), "capstone_model.h5")
scalerWeightPath = os.path.join(os.path.dirname(sys.argv[0]), "scaler.h5")
encoderPath = os.path.join(os.path.dirname(sys.argv[0]), "encoder.h5")

def convertToDict(jsonString: str):
    json_object = json.loads(jsonString)
    _json = json.dumps(json_object, indent=4)
    return json.loads(_json)

def predict(data, counter):
    model = load_model(modelWeightPath)
    predict=model.predict(data)
    with open(encoderPath,"rb") as f:
        encoder=pickle.load(f)

    saveprobabilitydistribution(predict[0], encoder, counter)

    predict = encoder.inverse_transform(predict)
    return predict  

def saveprobabilitydistribution(probabilities, encoder, counter):
    outputdirectory = os.path.join(os.getenv('APPDATA'), 'Capstone', 'Distribution');
    if not os.path.exists(os.path.join(outputdirectory)):
        os.mkdir(os.path.join(outputdirectory))

    color_data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    my_cmap = cm.get_cmap('jet')
    my_norm = Normalize(vmin=0, vmax=10)
    fig, ax = plt.subplots(figsize=(6, 4.5))
    prediction = probabilities.reshape((10,))
    ax.bar(x=encoder.categories_[0] , height=prediction,
            color=my_cmap(my_norm(color_data)))
    plt.xticks(rotation=45)
    ax.set_title(
        "Türler arası olasılık dağılımı")
    plt.savefig(os.path.join(outputdirectory, str(counter) + ".png"), bbox_inches='tight', dpi=200)


def find_nearest(array, value):
    array = np.asarray(array)
    idx = (np.abs(array - value)).argmin()
    return array[idx]

def getpredictionresult(rawJsonString, counter):
    featureDict = convertToDict(rawJsonString)
    features = pd.DataFrame.from_dict([featureDict])

    try:
        with open(scalerWeightPath, "rb") as f:
            scaler = pickle.load(f)
            features = scaler.transform(features)
    except Exception as e:
        print(str(e))
    features=np.expand_dims(features, axis=2)
    predictionResult = predict(features, counter)
    print(predictionResult.tolist()[0][0])

if __name__ == "__main__":
     args = sys.argv
     globals()[args[1]](*args[2:])