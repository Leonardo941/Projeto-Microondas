const API_URL = "http://microondasweb.azurewebsites.net/api";
const API_URL_AQUECIMENTO = `${API_URL}/aquecimento`;
const API_URL_PROGRAMA = `${API_URL}/programa`;

const contador = document.querySelector(".contador");
const statusAquecimento = document.getElementById("porta");
const potenciaDefault = 10;
const caractereDefault = ".";
const acrescimoDefault = 30;

let segundos = 0;
let tempoDigitado = 0;
let potencia = potenciaDefault;
let aquecendo = false;
let atualizandoStatus = false;
let entrada = caractereDefault;

const atualizarDisplay = (valor) => {
    contador.textContent = valor;
};

const resetaDisplay = () => {
    segundos = 0;
    tempoDigitado = 0;
    potencia = potenciaDefault;
    entrada = caractereDefault;

    atualizarDisplay(formatarTempo(0));
}

const atualizaSegundos = (tempoRestante) => {
    segundos = tempoRestante;
    atualizarDisplay(formatarTempo(segundos));
}

const formatarTempo = (segundos) => {
    if (isNaN(segundos) || segundos < 0) 
        return "00:00";

    let minutos = Math.floor(segundos / 60);
    let segundosRestantes = segundos % 60;
    
    return `${minutos.toString().padStart(2, "0")}:${segundosRestantes.toString().padStart(2, "0")}`;
};

document.querySelectorAll(".button-numpad").forEach(button => {
    if (!button.matches(".button-potencia") && !button.matches(".button-cadastrar")) { 
        button.addEventListener("click", () => {
            if (segundos.toString().length < 3 ) {
                segundos = parseInt(segundos.toString() + button.textContent);
                tempoDigitado = segundos;
                atualizarDisplay(formatarTempo(segundos));
            }
        });
    }
});

document.querySelector(".button-potencia").addEventListener("click", () => {
    if (!aquecendo && !segundos){
        potencia = potencia <= 1 ? potenciaDefault : potencia - 1;

        atualizarDisplay(`P: ${potencia}`);
    }
});

document.querySelector(".inicia-adiciona").addEventListener("click", () => {
    if (!aquecendo) {
        if (!segundos) {
            segundos = acrescimoDefault;
            tempoDigitado = segundos
        }
    } else {
        segundos += acrescimoDefault;
        tempoDigitado += acrescimoDefault;
    }

    iniciarAquecimento(segundos, potencia);
});

const iniciarAquecimento = (segundos, potencia) => {
    atualizaSegundos(segundos);

    fetch(`${API_URL_AQUECIMENTO}/aquecer`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Tempo: segundos, Potencia: potencia })
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(mensagem => {
                    exibirErro(mensagem);
                    
                    if (aquecendo) {
                        segundos -= acrescimoDefault;
                        tempoDigitado -= acrescimoDefault;
                    } else {
                        resetaDisplay();
                        atualizarStringInformativa(0);
                    }
                    
                    throw new Error(mensagem);
                });
            }
            aquecendo = true;
            atualizarStatus();
        })
        .catch(error => {
            console.error("Erro:", error);
        });
};

document.querySelector(".funcoes .cancela-pause").addEventListener("click", () => {
    if (aquecendo) {
        pausaAquecimento();
    } else if (!segundos)  {
        resetaDisplay();
        atualizarStringInformativa(0);
    } else {
        cancelaAquecimento();
    }
});

const pausaAquecimento = () => {
    fetch(`${API_URL_AQUECIMENTO}/pausar`, { method: "POST" })
        .then(() => {
            aquecendo = false;
        });
};

const cancelaAquecimento = () => {
    fetch(`${API_URL_AQUECIMENTO}/cancelar`, { method: "POST" })
        .then(() => {
            resetaDisplay()
            atualizarStringInformativa(0);
        });
};

const atualizarStatus = () => {

    if (atualizandoStatus)
        return;

    atualizandoStatus = true;

    const intervalo = setInterval(() => {
        fetch(`${API_URL_AQUECIMENTO}/status`)
            .then(response => response.json())
            .then(({ aquecimento, tempoRestante }) => {

                if (!aquecimento){
                    if (!tempoRestante){
                        atualizarStringInformativa(tempoRestante);
                        resetaDisplay();
                    } else {
                        atualizaSegundos(tempoRestante);
                        atualizarStringInformativa(tempoRestante);
                    }

                    clearInterval(intervalo);
                    aquecendo = false;

                } else {
                    atualizaSegundos(tempoRestante);
                    atualizarStringInformativa(tempoRestante);
                }
            })
            .catch(error => console.error("Erro ao obter status:", error));
    }, 1000);

    atualizandoStatus = false;
};

const atualizarStringInformativa = (tempoRestante) => {
    let pontos = ""

    for (let i=1; i <= (tempoDigitado - tempoRestante); i++){
        pontos += entrada.repeat(potencia) + " ";
    }

    if ((tempoDigitado - tempoRestante) == tempoDigitado){
        if (pontos)
            pontos += "Aquecimento concluído";
    }    

    statusAquecimento.textContent = pontos;
};

document.addEventListener("DOMContentLoaded", async function () {
    atualizaProgramas();
});

async function atualizaProgramas () {
    const buttonsContainer = document.querySelector(".programas");

    try {
        const response = await fetch(API_URL_PROGRAMA);

        if (!response.ok) {
            throw new Error("Erro ao carregar os programas.");
        }

        const programas = await response.json();
        buttonsContainer.innerHTML = "";

        programas.forEach(programa => {
            const button = criarBotaoPrograma(programa);
            buttonsContainer.appendChild(button);
        });

    } catch (error) {
        console.error(error);
    }
}

function criarBotaoPrograma(programa) {
    const button = document.createElement("button");
    button.classList.add("button-programa");
    button.textContent = programa.nome;
    
    if (programa.customizado) {
        const deleteIcon = document.createElement("span");
        deleteIcon.classList.add("delete-icon");
        deleteIcon.textContent = "X";
        button.appendChild(deleteIcon);
    
        deleteIcon.addEventListener("click", (event) => {
            event.stopPropagation(); // Impede que o evento de clique no botão também seja disparado
            if (confirm(`Deseja excluir o programa "${programa.nome}"?`)) {
                excluirPrograma(programa.id);
            }
        });
    }

    button.addEventListener("click", () => realizaAquecimentoPrograma(programa));
    
    return button;
}

function excluirPrograma(id) {
    fetch(`${API_URL_PROGRAMA}/${id}`, {
        method: "DELETE",
    })
    .then(response => {
        if (response.ok) {
            exibirSucesso("Programa excluído com sucesso!");
            atualizaProgramas();
        } else {
            throw new Error("Erro ao excluir o programa.");
        }
    })
    .catch(error => {
        exibirErro(error.message);
    });
}

const realizaAquecimentoPrograma = (programa) => {
    if (!aquecendo && !segundos) {

        segundos = parseInt(programa.tempo);
        tempoDigitado = segundos;
        potencia = parseInt(programa.potencia);
        entrada = programa.caractere;

        iniciarAquecimento(segundos, potencia);
    }
}

document.querySelector(".button-cadastrar").addEventListener("click", function() {
    const programasContainer = document.querySelector(".programas");
    const totalProgramas = programasContainer.querySelectorAll("button").length;

    if (totalProgramas >= 16) {
        exibirErro("Limite de programas alcançado. Não é possível cadastrar mais programas.");
    } else {
        if (!aquecendo && !segundos)
            document.getElementById("modalCadastro").style.display = "flex";
    }
});

document.querySelector(".button-cancelar-programa").addEventListener("click", function() {
    document.getElementById("modalCadastro").style.display = "none";
    document.getElementById("formCadastro").reset();
});

document.getElementById("formCadastro").addEventListener("submit", function(event) {
    event.preventDefault();
    
    const form = event.target;

    const programa = {
        nome: document.getElementById("nome").value,
        alimento: document.getElementById("alimento").value,
        potencia: document.getElementById("potencia").value,
        caractere: document.getElementById("caractere").value.replace('.', ''),
        tempo: document.getElementById("tempo").value,
        instrucao: document.getElementById("instrucao").value || ""
    };
    
    fetch(API_URL_PROGRAMA, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(programa)
    })
    .then(response => {
        if (!response.ok) {
            return response.text().then(mensagem => {
                exibirErro(mensagem);
                return Promise.reject(mensagem);
            });
        }
        return response.json();
    })
    .then(data => {
        document.getElementById("modalCadastro").style.display = "none";
        exibirSucesso("Programa cadastrado com sucesso!");
        atualizaProgramas();
        form.reset();
    })
    .catch(error => console.error("Erro ao cadastrar programa:", error));
});

const exibirErro = (mensagem) => {
    exibirMensagem(mensagem, "error-message");
};

const exibirSucesso = (mensagem) => {
    exibirMensagem(mensagem, "sucess-message");
};

const exibirMensagem = (mensagem, id) => {
    const errorMessageDiv = document.getElementById(id);
    errorMessageDiv.textContent = mensagem;
    errorMessageDiv.style.display = "block";

    setTimeout(() => {
        errorMessageDiv.style.display = "none";
    }, 5000);
}