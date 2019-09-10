//info: import required Lib 
import ace from "ace-builds/src-noconflict/ace";
import aceDraculaTheme from "ace-builds/src-noconflict/theme-dracula";
import aceWorkerJson from 'ace-builds/src-noconflict/worker-json'
import aceJsonMode from 'ace-builds/src-noconflict/mode-json'
import aceWebPack from 'ace-builds/webpack-resolver'



function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}


String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

if (!Array.prototype.last) {
    Array.prototype.last = function () {
        return this[this.length - 1];
    };
}

function copyTextToClipboard(text) {
    var myText = text.replaceAll("<br>", "\r\n").replaceAll("<br />", "\r\n").replaceAll("&gt;_", "");

    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(myText);
        return;
    }
    navigator.clipboard.writeText(myText).then(
        function () {
            //showToast({ html: "Copied" });
            //TODO: refactor 
            var msg = "<span>Copied</span>";
            M.toast({
                html: msg,
                classes: 'toast-success',
                displayLength: 4000
            })
        },
        function (err) {
            var msg = "Could not copy text: " + err;
            console.error(msg);
            showErrorToast(msg);
        }
    );

    // -- inner function

    function fallbackCopyTextToClipboard(text) {
        var textArea = document.createElement("textarea");
        textArea.value = text;
        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();

        try {
            var successful = document.execCommand("copy");
            var msg = successful ? "successful" : "unsuccessful";
            showToast({
                html: "Copied"
            });
        } catch (err) {
            var msg = "Could not copy text: " + err;
            console.error(msg);
            showErrorToast(msg);
        }

        document.body.removeChild(textArea);
    }
}

function initModal() {
    var elems = document.getElementsByClassName("modal");
    for (let index = 0; index < elems.length; index++) {
        M.Modal.init(elems[index]);
    }
}

function getModalInstance(ElementName) {
    var consolelElem = document.getElementById(ElementName);
    var instance = M.Modal.getInstance(consolelElem);


    return instance;
}

function initSideNav() {
    var elem = document.getElementById('slide-out');
    var instance = M.Sidenav.init(elem);
    return instance;
}

function initAceEditor() {
    var editor = ace.edit("editor");
    editor.getSession().setUseWorker(false);
    editor.setTheme(aceDraculaTheme);
    editor.setShowPrintMargin(false);
    editor.getSession().setMode("ace/mode/json");
    return editor;
}



function loadObjFile(objLink) {
    const canvas = document.querySelector('#c');
    console.log(canvas)
    const renderer = new THREE.WebGLRenderer({
        canvas
    });

    const fov = 45;
    const aspect = 2; // the canvas default
    const near = 0.1;
    const far = 100;
    const camera = new THREE.PerspectiveCamera(fov, aspect, near, far);
    camera.position.set(0, 10, 20);

    const controls = new THREE.OrbitControls(camera, canvas);
    controls.target.set(0, 5, 0);
    controls.update();

    const scene = new THREE.Scene();
    scene.background = new THREE.Color('white');



    {
        const planeSize = 4000;

        const loader = new THREE.TextureLoader();
        const texture = loader.load('https://threejsfundamentals.org/threejs/resources/images/checker.png');
        texture.wrapS = THREE.RepeatWrapping;
        texture.wrapT = THREE.RepeatWrapping;
        texture.magFilter = THREE.NearestFilter;
        const repeats = planeSize / 200;
        texture.repeat.set(repeats, repeats);

        const planeGeo = new THREE.PlaneBufferGeometry(planeSize, planeSize);
        const planeMat = new THREE.MeshPhongMaterial({
            map: texture,
            side: THREE.DoubleSide,
        });
        const mesh = new THREE.Mesh(planeGeo, planeMat);
        mesh.rotation.x = Math.PI * -.5;
        scene.add(mesh);
    }



    {
        const skyColor = 0xB1E1FF; // light blue
        const groundColor = 0xB97A20; // brownish orange
        const intensity = 1;
        const light = new THREE.HemisphereLight(skyColor, groundColor, intensity);
        scene.add(light);
    }

    {
        const color = 0xFFFFFF;
        const intensity = 1;
        const light = new THREE.DirectionalLight(color, intensity);
        light.position.set(5, 10, 2);
        scene.add(light);
        scene.add(light.target);
    }

    function frameArea(sizeToFitOnScreen, boxSize, boxCenter, camera) {
        const halfSizeToFitOnScreen = sizeToFitOnScreen * 0.5;
        const halfFovY = THREE.Math.degToRad(camera.fov * .5);
        const distance = halfSizeToFitOnScreen / Math.tan(halfFovY);
        // compute a unit vector that points in the direction the camera is now
        // in the xz plane from the center of the box
        const direction = (new THREE.Vector3())
            .subVectors(camera.position, boxCenter)
            .multiply(new THREE.Vector3(1, 0, 1))
            .normalize();

        // move the camera to a position distance units way from the center
        // in whatever direction the camera was from the center already
        camera.position.copy(direction.multiplyScalar(distance).add(boxCenter));

        // pick some near and far values for the frustum that
        // will contain the box.
        camera.near = boxSize / 100;
        camera.far = boxSize * 100;

        camera.updateProjectionMatrix();

        // point the camera to look at the center of the box
        camera.lookAt(boxCenter.x, boxCenter.y, boxCenter.z);
    }

    {
        const objLoader = new THREE.OBJLoader2();
        objLoader.load(objLink, (event) => {
            const root = event.detail.loaderRootNode;
            root.updateMatrixWorld();
            scene.add(root);
            // compute the box that contains all the stuff
            // from root and below
            const box = new THREE.Box3().setFromObject(root);

            const boxSize = box.getSize(new THREE.Vector3()).length();
            const boxCenter = box.getCenter(new THREE.Vector3());

            // set the camera to frame the box
            frameArea(boxSize * 1.2, boxSize, boxCenter, camera);

            // update the Trackball controls to handle the new size
            controls.maxDistance = boxSize * 10;
            controls.target.copy(boxCenter);
            controls.update();
        });
    }

    function resizeRendererToDisplaySize(renderer) {
        const canvas = renderer.domElement;
        const width = canvas.clientWidth;
        const height = canvas.clientHeight;
        const needResize = canvas.width !== width || canvas.height !== height;
        if (needResize) {
            renderer.setSize(width, height, false);
        }
        return needResize;
    }

    function render() {

        if (resizeRendererToDisplaySize(renderer)) {
            const canvas = renderer.domElement;
            camera.aspect = canvas.clientWidth / canvas.clientHeight;
            camera.updateProjectionMatrix();
        }

        renderer.render(scene, camera);

        requestAnimationFrame(render);
    }

    requestAnimationFrame(render);
}



export default {
    guid,
    copyTextToClipboard,
    initModal,
    getModalInstance,
    initAceEditor,
    initSideNav,
    loadObjFile
}