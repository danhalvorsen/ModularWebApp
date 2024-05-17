import "@spectrum-web-components/button/sp-button.js";
import "@spectrum-web-components/theme/sp-theme.js";
import { LitElement, TemplateResult, css, html } from "lit";

class MyComponent extends LitElement {
  static styles = css`
    :host {
      display: block;
      padding: 16px;
      font-family: var(--spectrum-alias-body-text-font-family);
    }
    .button {
      background-color: #04aa6d; /* Green */
      border: none;
      color: white;
      padding: 15px 32px;
      text-align: center;
      text-decoration: none;
      display: inline-block;
      font-size: 16px;
    }
  `;

  render(): TemplateResult {
    return html`
      <sp-theme theme="spectrum" scale="large">
        <sp-button variant="cta">Click Me</sp-button>
        <sp-button variant="accent">Click Me</sp-button>
        <sp-button variant="primary">Click Me</sp-button>
        <button class="button"></button>
      </sp-theme>
    `;
  }
}

customElements.define("my-component", MyComponent);
