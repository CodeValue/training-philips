// OrganLens web preview - plain JS, no framework.
(function () {
  'use strict';

  var currentStudyId = null;

  function $(selector) {
    return document.querySelector(selector);
  }

  function escapeHtml(text) {
    return String(text)
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;');
  }

  function formatValue(value, unit) {
    if (unit === '%') {
      return value + '%';
    }
    return value + ' ' + unit;
  }

  function showError(show) {
    $('#error-banner').classList.toggle('hidden', !show);
  }

  // ---------------------------------------------------------------- list

  async function loadStudies() {
    var response = await fetch('/api/studies');
    var studies = await response.json();
    var body = $('#study-list tbody');
    body.innerHTML = '';

    studies.forEach(function (s) {
      var row = document.createElement('tr');
      row.setAttribute('data-testid', 'study-row-' + s.id);
      row.className = 'clickable';
      row.innerHTML =
        '<td>' + escapeHtml(s.id) + '</td>' +
        '<td>' + escapeHtml(s.patientRef) + '</td>' +
        '<td>' + escapeHtml(s.modality) + '</td>' +
        '<td>' + escapeHtml(s.studyDate) + '</td>' +
        '<td>' + escapeHtml(s.organs.join(', ')) + '</td>' +
        '<td>' + escapeHtml(s.status) + '</td>';
      row.addEventListener('click', function () {
        openStudy(s.id);
      });
      body.appendChild(row);
    });
  }

  // ---------------------------------------------------------------- detail

  async function openStudy(id) {
    var response = await fetch('/api/studies/' + encodeURIComponent(id));
    if (!response.ok) {
      showError(true);
      return;
    }
    var study = await response.json();
    currentStudyId = study.id;

    $('#study-detail').classList.remove('hidden');
    $('#detail-title').textContent = study.id;
    $('#detail-meta').textContent = study.modality + ' · ' + study.studyDate + ' · ' + study.patientRef;
    $('#analysis-result').classList.add('hidden');
    $('#analysis-result').innerHTML = '';
    showError(false);

    var buttons = $('#analyze-buttons');
    buttons.innerHTML = '';
    study.organs.forEach(function (organ) {
      var button = document.createElement('button');
      button.type = 'button';
      button.textContent = 'Analyze ' + organ;
      button.setAttribute('data-testid', 'analyze-' + organ);
      button.addEventListener('click', function () {
        analyze(study.id, organ);
      });
      buttons.appendChild(button);
    });

    renderMeasurements(study.measurements);
  }

  async function analyze(studyId, organ) {
    var response = await fetch('/api/studies/' + encodeURIComponent(studyId) + '/analysis', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ organ: organ })
    });

    if (!response.ok) {
      showError(true);
      return;
    }

    var result = await response.json();
    var html = '<h4>' + escapeHtml(result.organ) + '</h4><ul class="measurements">';
    result.measurements.forEach(function (m) {
      html += '<li data-testid="measurement-' + escapeHtml(m.name) + '"><strong>' + escapeHtml(m.name) + ':</strong> ' + escapeHtml(formatValue(m.value, m.unit)) + '</li>';
    });
    html += '</ul>';

    if (result.findings.length === 0) {
      html += '<p class="note">No findings.</p>';
    } else {
      html += '<ul class="findings">';
      result.findings.forEach(function (f) {
        html += '<li data-testid="finding" class="sev-' + escapeHtml(f.severity) + '">' +
          '<code>' + escapeHtml(f.code) + '</code> ' + escapeHtml(f.description) +
          ' <span class="confidence">(confidence ' + Math.round(f.confidence * 100) + '%)</span></li>';
      });
      html += '</ul>';
    }

    var box = $('#analysis-result');
    box.innerHTML = html;
    box.classList.remove('hidden');
    loadStudies();
  }

  function renderMeasurements(measurements) {
    var body = $('#measurement-list tbody');
    body.innerHTML = '';
    measurements.forEach(function (m) {
      var row = document.createElement('tr');
      row.innerHTML =
        '<td>' + escapeHtml(m.organ) + '</td>' +
        '<td>' + escapeHtml(m.kind) + '</td>' +
        '<td>' + escapeHtml(m.value) + '</td>' +
        '<td>' + escapeHtml(m.unit) + '</td>' +
        '<td>' + escapeHtml(m.sliceThicknessMm) + '</td>' +
        '<td>' + escapeHtml(m.slicesCovered) + '</td>';
      body.appendChild(row);
    });
  }

  async function submitMeasurement(event) {
    event.preventDefault();
    if (!currentStudyId) return;

    var form = event.target;
    var payload = {
      organ: form.organ.value,
      kind: form.kind.value,
      value: Number(form.value.value),
      unit: form.unit.value,
      sliceThicknessMm: Number(form.sliceThicknessMm.value)
    };

    var response = await fetch('/api/studies/' + encodeURIComponent(currentStudyId) + '/measurements', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    });

    if (!response.ok) {
      // TODO: show the real error
      showError(true);
      return;
    }

    showError(false);
    form.reset();
    var list = await fetch('/api/studies/' + encodeURIComponent(currentStudyId) + '/measurements');
    renderMeasurements(await list.json());
  }

  document.addEventListener('DOMContentLoaded', function () {
    $('#measurement-form').addEventListener('submit', submitMeasurement);
    loadStudies();
  });
})();
