from flask import Flask, render_template, Response
from euclid import EuclidCamera
import argparse


app = Flask(__name__)

ap = argparse.ArgumentParser()
ap.add_argument("-a", "--host", required=True,
                help="host streaming address")
ap.add_argument("-s", "--source", required=True,
                help="source ip of camera stream")
ap.add_argument("-t", "--target", required=False, default='',
                help="path to recording floder")
ap.add_argument("-p", "--prototxt", required=True,
                help="path to Caffe 'deploy' prototxt file")
ap.add_argument("-m", "--model", required=True,
                help="path to Caffe pre-trained model")
ap.add_argument("-c", "--confidence", type=float, default=0.2,
                help="minimum probability to filter weak detections")
args = vars(ap.parse_args())


@app.route('/')
def index():
    return render_template('index.html')


def gen(camera):
    frame = camera.get_frame()
    camera.inferece_thread.start()
    while True:
        frame = camera.get_frame()
        yield (b'--frame\r\n'
               b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n\r\n')


@app.route('/video_feed')
def video_feed():
    return Response(gen(
        EuclidCamera(source=args["source"], target=args["target"], model=args["model"], weights=args["prototxt"],
                     confidence=args["confidence"])),
                    mimetype='multipart/x-mixed-replace; boundary=frame')


if __name__ == '__main__':
    # construct the argument parse and parse the arguments


    app.run(host=args["host"], threaded=True, debug=False)
