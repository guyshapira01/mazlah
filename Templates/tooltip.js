// Compact modern tooltip for Parts Map - Chrome
var timeout;
var theItem = null;
var lastMouseX = 0;
var lastMouseY = 0;

document.addEventListener('mousemove', function (e) {
    lastMouseX = e.clientX;
    lastMouseY = e.clientY;
}, true);

function getTipWindow() {
    return document.getElementById('tipwindow');
}

function getStatusBarFrame() {
    try { return top.frames['statusbar'] || null; } catch (e) { return null; }
}

function getStatusTextDiv() {
    var frame = getStatusBarFrame();
    if (!frame || !frame.document) return null;
    return frame.document.getElementById('textDiv')
        || frame.document.querySelector('[name="textDiv"]')
        || null;
}

function prepareTip(item) {
    item.setAttribute('dir', 'rtl');
    item.style.position = 'fixed';
    item.style.zIndex = '99999';
    item.style.display = 'none';

    item.style.width = 'auto';
    item.style.minWidth = '200px';
    item.style.maxWidth = '310px';

    item.style.minHeight = '72px';
    item.style.padding = '12px 16px';
    item.style.boxSizing = 'border-box';

    item.style.background = 'rgba(18, 49, 86, 0.97)';
    item.style.border = '1px solid rgba(255,255,255,.22)';
    item.style.borderRadius = '8px';
    item.style.boxShadow = '0 5px 16px rgba(0,0,0,.25)';

    item.style.color = '#ffffff';
    item.style.fontFamily = '"Segoe UI", Arial, sans-serif';
    item.style.fontSize = '13px';
    item.style.lineHeight = '1.45';
    item.style.textAlign = 'right';

    item.style.pointerEvents = 'none';
}

function positionTip(item, evt) {
    var x = lastMouseX;
    var y = lastMouseY;

    if (evt) {
        if (typeof evt.clientX === 'number') x = evt.clientX;
        if (typeof evt.clientY === 'number') y = evt.clientY;
    }

    var gap = 14;
    var vw = document.documentElement.clientWidth || window.innerWidth;
    var vh = document.documentElement.clientHeight || window.innerHeight;

    // Need visible first so getBoundingClientRect returns real size.
    item.style.visibility = 'hidden';
    item.style.display = 'block';

    var rect = item.getBoundingClientRect();

    // Prefer centered ABOVE the hotspot/cursor.
    var left = x - (rect.width / 2);
    var top = y - rect.height - gap;

    // Clamp horizontally.
    left = Math.max(8, Math.min(left, vw - rect.width - 8));

    // If no room above, place below.
    if (top < 8) {
        top = Math.min(vh - rect.height - 8, y + gap);
    }

    item.style.left = Math.round(left) + 'px';
    item.style.top = Math.round(top) + 'px';
    item.style.visibility = 'visible';
}

function ToolTip(ToolTipText, evt) {
    var item = getTipWindow();
    if (!item) return;

    theItem = item;
    prepareTip(item);

    item.innerHTML =
        '<div style="font-size:14px;font-weight:700;color:#fff;white-space:normal;">'
        + escapeHtml(ToolTipText || '') +
        '</div>';

    positionTip(item, evt);

    clearTimeout(timeout);
    timeout = setTimeout(HideToolTip, 50000);
}

function ToolTipMultiLine(LineStrings, evt) {
    if (!LineStrings) return;

    var item = getTipWindow();
    if (!item) return;

    theItem = item;
    prepareTip(item);

    var rows = String(LineStrings).split('|');
    var parsed = [];

    for (var i = 0; i < rows.length; i++) {
        if (!rows[i]) continue;

        var parts = rows[i].split('^');
        parsed.push({
            value: parts.length > 0 ? parts[0] : '',
            label: parts.length > 1 ? parts[1] : ''
        });
    }

    var html = '';

    // First value is treated as the item name/title.
    if (parsed.length > 0) {
        html += '<div style="font-size:14px;font-weight:700;color:#fff;'
             + 'line-height:1.45;min-height:22px;white-space:normal;overflow-wrap:anywhere;">'
             + escapeHtml(parsed[0].value)
             + '</div>';
    }

    // Remaining values are compact metadata rows.
    for (var n = 1; n < parsed.length; n++) {
        html += '<div style="margin-top:8px;padding-top:7px;'
             + 'border-top:1px solid rgba(255,255,255,.14);'
             + 'font-size:12px;color:#d7e8fa;">';

        if (parsed[n].label) {
            html += '<span style="color:#9fc3ea;font-weight:600;">'
                 + escapeHtml(parsed[n].label)
                 + ': </span>';
        }

        html += '<span style="color:#fff;font-weight:600;direction:ltr;display:inline-block;">'
             + escapeHtml(parsed[n].value)
             + '</span></div>';
    }

    item.innerHTML = html;
    positionTip(item, evt);

    // Old statusbar must remain empty.
    var statusItem = getStatusTextDiv();
    if (statusItem) {
        statusItem.innerHTML = '';
        statusItem.style.display = 'none';
    }

    clearTimeout(timeout);
    timeout = setTimeout(HideToolTip, 50000);
}

function ShowToolTip() {
    if (theItem) {
        theItem.style.display = 'block';
        positionTip(theItem, null);
    }
}

function HideToolTip() {
    clearTimeout(timeout);

    var item = getTipWindow();
    if (item) item.style.display = 'none';

    var statusItem = getStatusTextDiv();
    if (statusItem) {
        statusItem.innerHTML = '';
        statusItem.style.display = 'none';
    }

    theItem = null;
}

function escapeHtml(value) {
    return String(value)
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#039;');
}
