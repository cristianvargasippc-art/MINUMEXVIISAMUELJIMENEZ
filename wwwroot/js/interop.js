window.delegame = {
    video() {
        return document.getElementById('introVid');
    },
    playIntro() {
        const video = this.video();
        if (!video) return;
        video.muted = true;
        video.loop = true;
        const started = video.play();
        if (!started) return;
        started.then(() => { video.classList.add('playing'); })
            .catch(() => { video.style.opacity = '0.25'; });
    },
    enableSound() {
        const video = this.video();
        if (!video) return;
        video.muted = false;
        video.volume = 0.9;
        if (video.paused) video.play().then(() => video.classList.add('playing')).catch(() => { });
    },
    stopIntro() {
        const video = this.video();
        if (video) video.pause();
    },
    scrollTop() {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }
};
