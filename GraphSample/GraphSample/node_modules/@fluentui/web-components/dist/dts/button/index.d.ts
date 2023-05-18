import { Button } from '@microsoft/fast-foundation';
/**
 * Types of button appearance.
 * @public
 */
export declare type ButtonAppearance = 'accent' | 'lightweight' | 'neutral' | 'outline' | 'stealth';
/**
 * The Fluent Button Element. Implements {@link @microsoft/fast-foundation#Button},
 * {@link @microsoft/fast-foundation#ButtonTemplate}
 *
 *
 * @public
 * @remarks
 * HTML Element: \<fluent-button\>
 *
 * {@link https://developer.mozilla.org/en-US/docs/Web/API/ShadowRoot/delegatesFocus | delegatesFocus}
 */
export declare class FluentButton extends Button {
    /**
     * The appearance the button should have.
     *
     * @public
     * @remarks
     * HTML Attribute: appearance
     */
    appearance: ButtonAppearance;
    appearanceChanged(oldValue: ButtonAppearance, newValue: ButtonAppearance): void;
    /**
     * @internal
     */
    connectedCallback(): void;
    /**
     * Applies 'icon-only' class when there is only an SVG in the default slot
     *
     * @internal
     */
    defaultSlottedContentChanged(): void;
}
/**
 * Styles for Button
 * @public
 */
export declare const ButtonStyles: import("@microsoft/fast-element").ElementStyles;
