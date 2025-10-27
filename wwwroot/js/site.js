

/**
 * @param {string} texto
 * @param {'ok'|'warn'|'info'|'error'} tipo
 */
export function mostrarToast(texto, tipo = 'info') {
    const wrap = document.getElementById('toast-wrap') || (() => {
        const w = document.createElement('div');
        w.id = 'toast-wrap';
        w.style.position = 'fixed';
        w.style.right = '16px';
        w.style.bottom = '16px';
        w.style.display = 'grid';
        w.style.gap = '8px';
        w.style.zIndex = 9999;
        document.body.appendChild(w);
        return w;
    })();

    const t = document.createElement('div');
    t.textContent = texto;
    t.style.padding = '10px 12px';
    t.style.borderRadius = '12px';
    t.style.color = '#e5e7eb';
    t.style.border = '1px solid #1f2937';
    t.style.background = { ok: '#065f46', warn: '#7c2d12', info: '#1e3a8a', error: '#7f1d1d' }[tipo] || '#1e3a8a';
    t.style.boxShadow = '0 10px 24px rgba(0,0,0,.35)';
    t.style.opacity = '0';
    t.style.transform = 'translateY(6px)';
    t.style.transition = 'opacity .18s ease, transform .18s ease';
    wrap.appendChild(t);
    requestAnimationFrame(() => { t.style.opacity = '1'; t.style.transform = 'none'; });
    setTimeout(() => {
        t.style.opacity = '0'; t.style.transform = 'translateY(6px)';
        setTimeout(() => t.remove(), 200);
    }, 2800);
}


window.addEventListener('DOMContentLoaded', () => {
    const msj = document.querySelector('[data-mensaje-ok]');
    if (msj) mostrarToast(msj.getAttribute('data-mensaje-ok'), 'ok');
});
