// Comunicação com a Web API (mesma origem: http://localhost:5080)
const API = '/api/livros';

const form = document.getElementById('form-livro');
const campos = {
  id: document.getElementById('livro-id'),
  titulo: document.getElementById('titulo'),
  autor: document.getElementById('autor'),
  isbn: document.getElementById('isbn'),
  ano: document.getElementById('ano'),
  paginas: document.getElementById('paginas')
};
const tituloForm = document.getElementById('titulo-form');
const btnSalvar = document.getElementById('btn-salvar');
const btnCancelar = document.getElementById('btn-cancelar');
const erroForm = document.getElementById('erro-form');
const corpoTabela = document.getElementById('corpo-tabela');
const vazio = document.getElementById('vazio');
const contador = document.getElementById('contador');
const busca = document.getElementById('busca');
const aviso = document.getElementById('aviso');

let livros = [];
let temporizadorAviso;
let temporizadorBusca;

// ---------- Utilidades ----------
function mostrarAviso(texto, ehErro = false) {
  aviso.textContent = texto;
  aviso.classList.toggle('erro-aviso', ehErro);
  aviso.hidden = false;
  clearTimeout(temporizadorAviso);
  temporizadorAviso = setTimeout(() => (aviso.hidden = true), 3500);
}

function mostrarErroForm(mensagem) {
  erroForm.textContent = mensagem;
  erroForm.hidden = false;
}

function limparErroForm() {
  erroForm.hidden = true;
  Object.values(campos).forEach(c => c.classList.remove('invalido'));
}

// Extrai a mensagem de erro devolvida pela API (ProblemDetails ou erros de validação)
async function lerErro(resposta) {
  try {
    const corpo = await resposta.json();
    if (corpo.errors) return Object.values(corpo.errors).flat().join(' ');
    return corpo.detail ? `${corpo.title} ${corpo.detail}` : corpo.title || 'Erro inesperado.';
  } catch {
    return 'Não foi possível processar a resposta do servidor.';
  }
}

function criarCelula(texto, classe) {
  const td = document.createElement('td');
  td.textContent = texto;
  if (classe) td.className = classe;
  return td;
}

// ---------- READ ----------
async function carregarLivros() {
  try {
    const termo = busca.value.trim();
    const url = termo ? `${API}?busca=${encodeURIComponent(termo)}` : API;
    const resposta = await fetch(url);

    if (!resposta.ok) throw new Error(await lerErro(resposta));

    livros = await resposta.json();
    renderizarTabela();
  } catch (erro) {
    mostrarAviso(`Não foi possível carregar os livros. ${erro.message}`, true);
  }
}

function renderizarTabela() {
  corpoTabela.replaceChildren();
  contador.textContent = livros.length;
  vazio.hidden = livros.length > 0;

  for (const livro of livros) {
    const tr = document.createElement('tr');
    tr.append(
      criarCelula(livro.titulo, 'titulo'),
      criarCelula(livro.autor),
      criarCelula(livro.isbnFormatado),
      criarCelula(livro.anoPublicacao),
      criarCelula(livro.tempoLeituraEstimado)
    );

    const acoes = document.createElement('td');
    acoes.className = 'acoes-linha';

    const btnEditar = document.createElement('button');
    btnEditar.type = 'button';
    btnEditar.className = 'secundario pequeno';
    btnEditar.textContent = 'Editar';
    btnEditar.addEventListener('click', () => iniciarEdicao(livro));

    const btnExcluir = document.createElement('button');
    btnExcluir.type = 'button';
    btnExcluir.className = 'perigo pequeno';
    btnExcluir.textContent = 'Excluir';
    btnExcluir.addEventListener('click', () => excluirLivro(livro));

    acoes.append(btnEditar, btnExcluir);
    tr.append(acoes);
    corpoTabela.append(tr);
  }
}

// ---------- CREATE / UPDATE ----------
form.addEventListener('submit', async (evento) => {
  evento.preventDefault();
  limparErroForm();

  // Validação básica no navegador
  const vazios = ['titulo', 'autor', 'isbn', 'ano', 'paginas'].filter(n => !campos[n].value.trim());
  if (vazios.length) {
    vazios.forEach(n => campos[n].classList.add('invalido'));
    mostrarErroForm('Preencha todos os campos antes de salvar.');
    return;
  }

  const dados = {
    titulo: campos.titulo.value.trim(),
    autor: campos.autor.value.trim(),
    isbn: campos.isbn.value.trim(),
    anoPublicacao: Number(campos.ano.value),
    numeroPaginas: Number(campos.paginas.value)
  };

  const id = campos.id.value;
  const editando = Boolean(id);

  btnSalvar.disabled = true;

  try {
    const resposta = await fetch(editando ? `${API}/${id}` : API, {
      method: editando ? 'PUT' : 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dados)
    });

    if (!resposta.ok) {
      mostrarErroForm(await lerErro(resposta));
      return;
    }

    mostrarAviso(editando ? 'Livro atualizado.' : 'Livro cadastrado.');
    sairDoModoEdicao();
    await carregarLivros();
  } catch {
    mostrarErroForm('Não foi possível conectar à API. Verifique se ela está em execução.');
  } finally {
    btnSalvar.disabled = false;
  }
});

function iniciarEdicao(livro) {
  limparErroForm();
  campos.id.value = livro.id;
  campos.titulo.value = livro.titulo;
  campos.autor.value = livro.autor;
  campos.isbn.value = livro.isbn;
  campos.ano.value = livro.anoPublicacao;
  campos.paginas.value = livro.numeroPaginas;

  tituloForm.textContent = 'Editar livro';
  btnSalvar.textContent = 'Salvar alterações';
  btnCancelar.hidden = false;
  campos.titulo.focus();
}

function sairDoModoEdicao() {
  form.reset();
  campos.id.value = '';
  tituloForm.textContent = 'Novo livro';
  btnSalvar.textContent = 'Cadastrar livro';
  btnCancelar.hidden = true;
  limparErroForm();
}

btnCancelar.addEventListener('click', sairDoModoEdicao);

// ---------- DELETE ----------
async function excluirLivro(livro) {
  if (!confirm(`Excluir o livro "${livro.titulo}"? Esta ação não pode ser desfeita.`)) return;

  try {
    const resposta = await fetch(`${API}/${livro.id}`, { method: 'DELETE' });

    if (!resposta.ok) {
      mostrarAviso(await lerErro(resposta), true);
      return;
    }

    if (campos.id.value === String(livro.id)) sairDoModoEdicao();
    mostrarAviso('Livro excluído.');
    await carregarLivros();
  } catch {
    mostrarAviso('Não foi possível conectar à API.', true);
  }
}

// ---------- Busca ----------
busca.addEventListener('input', () => {
  clearTimeout(temporizadorBusca);
  temporizadorBusca = setTimeout(carregarLivros, 250);
});

carregarLivros();
