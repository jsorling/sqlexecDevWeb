require.config({ paths: { vs: '/lib/monaco-editor/min/vs' } });

require(['vs/editor/editor.main'], function () {
    $('[data-lang]').each(function () {
        var theme = getStoredTheme = localStorage.getItem('theme');

        if ((theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches) || theme === 'dark') {
            monaco.editor.colorizeElement($(this).get(0), { theme: 'vs-dark' })
        } else {
            monaco.editor.colorizeElement($(this).get(0))
        }
    });
    $('[data-editor]').each(function () {
        var theme = getStoredTheme = localStorage.getItem('theme');
        var lang = $(this).data('editor')
        var metheme = (theme === 'auto' && window.matchMedia('(prefers-color-scheme: dark)').matches) || theme === 'dark' ? 'vs-dark' : ''


        monacoEditor = monaco.editor
            .create(
                $(this).get(0), {
                contextmenu: true,
                language: lang,
                theme: metheme,
                minimap: { enabled: false }
            })

        monacoEditor.updateOptions({ readOnly: false });
        $(window).on("resize", function () {
            monacoEditor.layout();
        });

        var inputSource = $(this).data('inputsource')
        if (typeof inputSource !== 'undefined') {
            var inputElm = $('textarea#' + inputSource)[0]
            if (typeof inputElm !== 'undefined') {
                monacoEditor.getModel().setValue(inputElm.value);

                $('FORM').each(function (i, obj) {
                    obj.addEventListener('submit', (event) => {
                        inputElm.value = monacoEditor.getModel().getValue();
                        return false;
                    })
                })
            }
        }
    });
});